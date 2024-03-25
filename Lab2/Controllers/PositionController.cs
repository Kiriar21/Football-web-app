using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class PositionController : Controller
    {
        private readonly Database _dbContext;

        public PositionController(Database dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Positions.ToList());
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Added(string Name)
        {
            Position position = new Position()
            {
                Name = Name,
            };
            
            _dbContext.Positions.Add(position);
            _dbContext.SaveChanges();

            return View(position);
        }
    }
}
