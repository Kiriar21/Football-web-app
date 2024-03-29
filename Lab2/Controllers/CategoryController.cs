using Microsoft.AspNetCore.Mvc;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Database _dbContext;

        public CategoryController(Database dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Categories.ToList());
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Added(string Name)
        {
            Category category = new Category()
            {
                Name = Name
            };

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
