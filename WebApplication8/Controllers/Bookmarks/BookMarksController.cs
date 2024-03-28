using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace WebApplication8.Controllers.Bookmarks
{
    public class BookMarksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult JsonArray()
        {
            return Json(new { message = "valid" });
        }
    }
}
