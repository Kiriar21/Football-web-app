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
        public IActionResult Added(string Name, string Country, string City, string FoundingDate, string LeagueId )
        {
            Team team;
            try
            {
                if (int.TryParse(LeagueId, out int parseLeagueId) &&
                _dbcontext.Leagues.First(x => x.LeagueId == parseLeagueId) != null)
                {

                    if(DateTime.TryParse(FoundingDate, out DateTime parseFoundingDate)) {

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
            } catch (Exception ex)
            {
                //return Problem("coś poszło nie tak");
            }

            return RedirectToAction("Index");
        }
    }
}
