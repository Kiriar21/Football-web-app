using Microsoft.AspNetCore.Mvc;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab2.Controllers
{
    public class TeamController : Controller
    {
        private readonly Database _dbcontext;

        public TeamController(Database dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View(_dbcontext.Teams.ToList());
        }
        public IActionResult Add()
        {
            var tempLeagues = _dbcontext.Leagues.ToList();
            var leagues = new List<SelectListItem>();
            foreach (var league in tempLeagues)
            {
                string id = league.LeagueId.ToString();
                string name = league.Name.ToString() + " (" + league.Country + ")";
                leagues.Add(new SelectListItem(name, id));
            }
            ViewBag.leagues = leagues;
            return View();
        }
        public IActionResult Added(string Name, string Country, string City, string FoundingDate, string LeagueId)
        {
            Team team;
            if (int.TryParse(LeagueId, out int parseLeagueId) &&
            _dbcontext.Leagues.First(x => x.LeagueId == parseLeagueId) != null)
            {

                if (DateTime.TryParse(FoundingDate, out DateTime parseFoundingDate))
                {

                    team = new()
                    {
                        Name = Name,
                        Country = Country,
                        City = City,
                        FoundingDate = parseFoundingDate,
                        LeagueId = parseLeagueId,
                    };

                    _dbcontext.Teams.Add(team);
                    _dbcontext.SaveChanges();

                }
            }


            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (_dbcontext.Teams.FirstOrDefault(x => x.TeamId == id) != null)
            {
                var leaguesTemp = _dbcontext.Leagues.ToList();
                var leagues = new List<SelectListItem>();
                foreach (var team in leaguesTemp)
                {
                    string leagueId = team.LeagueId.ToString();
                    string leagueName = team.Name.ToString();
                    leagues.Add(new SelectListItem(leagueName, leagueId));
                }
                ViewBag.Teams = leagues;
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(_dbcontext.Teams.FirstOrDefault(x => x.TeamId == id));
        }
        [HttpPost]
        public IActionResult Edit(Team team)
        {
            if (_dbcontext.Teams!.FirstOrDefault(x => x.TeamId == team.TeamId) != null)
            {
                var mainTeam = _dbcontext.Teams!.FirstOrDefault(x => x.TeamId == team.TeamId);

                if (mainTeam != null)
                {
                    mainTeam.Name = team.Name;
                    mainTeam.Country = team.Country;
                    mainTeam.City = team.City;
                    mainTeam.FoundingDate = team.FoundingDate;
                    mainTeam.LeagueId = team.LeagueId;
                }
                _dbcontext.SaveChanges();
            }
            else
            {
                return RedirectToAction("Player", "Index");
            }

            return RedirectToAction("Index");
        }
        public IActionResult ShowPlayers(int id)
        {
            if (_dbcontext.Teams.FirstOrDefault(x => x.TeamId == id) != null)
            {
                return View(_dbcontext.Teams.FirstOrDefault(x => x.TeamId == id));
            }
            return RedirectToAction("Index");
        }
    }
}
