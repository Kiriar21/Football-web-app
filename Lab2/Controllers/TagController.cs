using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class TagController : Controller
    {
        private readonly Database _dbContext;

        public TagController(Database dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Tags.ToList());
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Added(string Name)
        {
            Tag tag = new Tag()
            {
                Name = Name
            };

            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();

            return View(tag);
        }
    }
}
