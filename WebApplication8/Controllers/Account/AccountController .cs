using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Controllers.Notify;
using WebApplication8.Data;
using WebApplication8.Filters;
using WebApplication8.Models.Account;
using WebApplication8.Models.Video;

namespace WebApplication8.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        //private readonly NotifyController _notifyController;
        private readonly Notifaction _notifaction;

        public List<String> _loggedInUsers;

        public AccountController(ApplicationDbContext context, Notifaction notifaction)
        {
            _context = context;
            _notifaction = notifaction;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Models.Account.Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
                if (user != null)
                {


                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserName", user.Email.ToString());
                    // return RedirectToAction("JsonRetrun");
                    return RedirectToAction("Index", "Chennel");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(login);
                }
            }

            return View(login);
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(Models.Account.Register register)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == register.Email);
                if (existingUser == null)
                {
                    _context.Users.Add(new User { Email = register.Email, Password = register.Password });
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Registration successful. Please login.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "User already exists.");
                    return View(register);
                }
            }

            return View(register);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("LearnNext");
            return RedirectToAction("Login");
        }
    

        //public JsonResult JsonRetrun()
        //{

        //    var currentEmail = HttpContext.Session.GetString("UserName");
        //    return Json(new { Email = currentEmail, Message = "Successfully logged in ", Title = "Login in Message" });
        //}
    }

}
