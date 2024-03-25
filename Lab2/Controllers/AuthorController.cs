using Microsoft.AspNetCore.Mvc;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class AuthorController : Controller
    {
        private readonly Database _dbContext;

        public AuthorController(Database dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Authors.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Added(string FirstName, string LastName)
        {

            Author author = new Author()
            {
                FirstName = FirstName,
                LastName = LastName
            };

            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();

            return View(author);

        }
    }
}
