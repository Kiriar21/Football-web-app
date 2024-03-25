using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class MatchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
