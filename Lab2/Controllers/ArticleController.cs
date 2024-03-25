using Microsoft.AspNetCore.Mvc;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index(int id = 1)
        {
            var article = new List<Article>
            {
                new() {
                    ArticleId = 1,
                    Title = "Artykul 1",
                    Content = "Tresc art 1",
                    CreationDate = DateTime.Now
                },
                new() {
                    ArticleId = 2,
                    Title = "Artykul 2",
                    Content = "Tresc art 2",
                    CreationDate = DateTime.Now
                },
                new() {
                    ArticleId = 3,
                    Title = "Artykul 3",
                    Content = "Tresc art 3",
                    CreationDate = DateTime.Now
                },
            };

            Article art;
            try {
                art = article[id - 1];
                
            } catch (Exception ex) {
                art = new Article();
                Console.WriteLine("Exception: " + ex);
            }
            return View(art);
            
        }
    }
}
