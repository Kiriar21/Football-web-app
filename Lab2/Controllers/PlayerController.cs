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

          
            if (int.TryParse(TeamId, out int parseTeamId) &&
                _dbContext.Teams.FirstOrDefault(x => x.TeamId == parseTeamId) != null) 
            {
                var positions = _dbContext.Positions.Where(x => Positions.Contains(x.PositionId)).ToList();
                DateTime.TryParse(BirthDate, out DateTime parseBirthDate);
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
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int PlayerId = -1)
        {
            if (_dbContext.Players.FirstOrDefault(x => x.PlayerId == PlayerId) != null)
            {

                var tempTeams = _dbContext.Teams.ToList();
                var teams = new List<SelectListItem>();
                foreach (var team in tempTeams)
                {
                    string idTeam = team.TeamId.ToString();
                    string name = team.Name.ToString();
                    teams.Add(new SelectListItem(name, idTeam));
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

                return View(_dbContext.Players!.FirstOrDefault(x => x.PlayerId == PlayerId));
            } 
            else
            {
                return RedirectToAction("Index");
            }
           
        }
        [HttpPost]
        public IActionResult EditUpdate(Player player, List<int> Positions)
        {
            
            if(_dbContext.Players.Find(player.PlayerId) != null)
            {
                var playerTemp = _dbContext.Players.FirstOrDefault(x => x.PlayerId == player.PlayerId);
                var positions = _dbContext.Positions.Where(x => Positions.Contains(x.PositionId)).ToList();

                if (playerTemp != null)
                {
                    playerTemp.FIrstName = player.FIrstName.ToString();
                    playerTemp.LastName = player.LastName.ToString();
                    playerTemp.BirthDate = player.BirthDate;
                    playerTemp.Country = player.Country.ToString();
                    playerTemp.TeamId = player.TeamId;

                    foreach (var newPosition in Positions)
                    {
                        foreach (var oldPosition in playerTemp.Positions)
                        {
                            if(newPosition != oldPosition.PositionId)
                            {
                                playerTemp.Positions.Remove(oldPosition);
                            }
                        }
                    }

                    playerTemp.Positions = positions;
                    _dbContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
