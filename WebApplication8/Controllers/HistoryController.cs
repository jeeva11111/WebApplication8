using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TrackHistory()
        {
            return View();
        }
        public IActionResult DeleteHistory()
        {
            return View();
        }
    }
}
