using Microsoft.AspNetCore.Mvc;
using WebApplication8.Data;

namespace WebApplication8.Controllers.Message
{
    public class MessageController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost, Route("/sendMessage")]
        public IActionResult SendMessage()
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            if (currentUser != null)
            {
                var message = "";
            }
            return Ok(new { textMessage = currentUser });
        }
    }
}
