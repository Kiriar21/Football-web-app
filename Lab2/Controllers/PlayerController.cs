using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab2.Controllers
{
    public class PlayerController : Controller
    {
        private readonly Database _dbContext;
        public PlayerController( Database dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Players.ToList());
        }
        public IActionResult Add()
        {
            var tempTeams = _dbContext.Teams.ToList();
            var teams = new List<SelectListItem>();
            foreach (var team in tempTeams)
            {
                string id = team.TeamId.ToString();
                string name = team.Name.ToString();
                teams.Add(new SelectListItem(name, id));
            }
            ViewBag.Teams = teams;
            var tempPositions = _dbContext.Positions.ToList();
            var positions = new List<SelectListItem>();
            foreach (var position in tempPositions)
            {
                string id = position.PositionId.ToString();
                string name = position.Name.ToString();
                positions.Add(new SelectListItem(name, id));
            }
            ViewBag.Positions = positions;
            return View();
        }
        [HttpPost]
        public IActionResult Added(string FIrstName, string LastName, string Country, string BirthDate, string TeamId, List<int> Positions) 
        {
            Player player;

            try
            {
              if(DateTime.TryParse(BirthDate, out DateTime parseBirthDate))
                {
                    if (int.TryParse(TeamId, out int parseTeamId) &&
                        _dbContext.Teams.FirstOrDefault(x => x.TeamId == parseTeamId) != null) 
                    {
                        var positions = _dbContext.Positions.Where(x => Positions.Contains(x.PositionId)).ToList();

                        player = new()
                        {
                            FIrstName = FIrstName,
                            LastName = LastName,
                            Country = Country,
                            BirthDate = parseBirthDate,
                            TeamId = parseTeamId,
                            Positions = positions
                        };

                        _dbContext.Players.Add(player);
                        _dbContext.SaveChanges();
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            // dodawanie danych 
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int PlayerId = -1)
        {
            if (_dbContext.Players.Find(PlayerId) != null)
            {
                return View(_dbContext.Players.FirstOrDefault(x => x.PlayerId == PlayerId));
            } 
            else
            {
                return View("Add");
            }
        }
        [HttpPost]
        public IActionResult Edit(Player player)
        {
            if(_dbContext.Players.Find(player.PlayerId) != null)
            {
                var authorTemp = _dbContext.Players.FirstOrDefault(x => x.PlayerId == player.PlayerId);

                if(authorTemp != null)
                {
                    authorTemp.FIrstName = player.FIrstName;
                    authorTemp.LastName = player.LastName;
                    authorTemp.BirthDate = player.BirthDate;
                    authorTemp.MatchPlayers+
                }
            }

            return RedirectToAction("Index");
        }
    }
}
