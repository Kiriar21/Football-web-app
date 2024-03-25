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

                var tempPlayer = _dbContext.Players.FirstOrDefault(x => x.PlayerId == PlayerId);
                var tempPlayerPositions = tempPlayer!.Positions.ToList();

                var tempPositions = _dbContext.Positions;
                var positions = new List<SelectListItem>();
                foreach (var position in tempPositions)
                {
                    string idPosition = position.PositionId.ToString();
                    string name = position.Name.ToString();
                    var selectedItem = new SelectListItem(name, idPosition);
                    selectedItem.Selected = true;

                    //foreach(var pos in tempPlayerPositions)
                    //{   
                    //    if(pos.PositionId == position.PositionId)
                    //    {
                    //        selectedItem.Selected = true;

                    //    }
                    //}
                    positions.Add(selectedItem);
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
        public IActionResult EditUpdate(Player player)
        {
            
            if(_dbContext.Players.Find(player.PlayerId) != null)
            {
                var playerTemp = _dbContext.Players.FirstOrDefault(x => x.PlayerId == player.PlayerId);

                if(playerTemp != null)
                {
                    playerTemp.FIrstName = player.FIrstName.ToString();
                    playerTemp.LastName = player.LastName.ToString();
                    playerTemp.BirthDate = player.BirthDate;
                    playerTemp.Country = player.Country.ToString();
                    playerTemp.TeamId = player.TeamId;
                    playerTemp.Positions = player.Positions.ToList();
                }
                
                _dbContext.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}
