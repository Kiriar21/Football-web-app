using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
