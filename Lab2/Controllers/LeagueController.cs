using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class LeagueController : Controller
    {
        private readonly Database _dbContext;

        public LeagueController(Database dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Leagues.ToList());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Added(string Name, string Country, int Level)
        {
            League league = new League()
            {
                Name = Name,
                Country = Country,
                Level = Level
            };

            _dbContext.Leagues.Add(league);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var leagueTemp = _dbContext.Leagues.FirstOrDefault(x => x.LeagueId == id);
            if (leagueTemp != null)
            {
                _dbContext.Leagues.Remove(leagueTemp);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
