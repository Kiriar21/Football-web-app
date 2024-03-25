using Microsoft.AspNetCore.Mvc;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly Database _dbContext;

        public EventTypeController(Database dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.EventTypes.ToList());
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Added(string name)
        {
            EventType eventType = new()
            {
                Name = name,
            };

            _dbContext.EventTypes.Add(eventType);
            _dbContext.SaveChanges();

            return View(eventType);
        }
    }
}
