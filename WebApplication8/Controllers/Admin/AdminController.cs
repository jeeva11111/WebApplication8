using Microsoft.AspNetCore.Mvc;
using WebApplication8.Data;
using WebApplication8.Models.Video;

namespace WebApplication8.Controllers.Login
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var checkValidLogin = _context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            if (checkValidLogin != null)
            {
                return Json(new { message = false });
            };
            ViewBag.LoginInfo = "Invalid Email or password";

            return Json(new { message = true });
        }

        public IActionResult Login()
        {
            return View();
        }

   
    }
}
