using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WebApplication8.Data;
using WebApplication8.Filters;
using WebApplication8.Models.Account.Profile;
using WebApplication8.Models.DTO;
using WebApplication8.Models.Video;

namespace WebApplication8.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public List<String> _loggedInUsers;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;

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
                    HttpContext.Items["CurrentUser"] = user.Name;
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


        public JsonResult JsonRetrun()
        {

            var currentEmail = HttpContext.Session.GetString("UserName");
            return Json(new { Email = currentEmail, Message = "Successfully logged in ", Title = "Login in Message" });
        }


        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]

        public IActionResult ProfileCardDetails()
        {

            UserProfileDTO userProfileCount = new UserProfileDTO();
            string userIdString = HttpContext.Session.GetString("UserId");

            int userId = Convert.ToInt32(userIdString);
            var userChannel = _context.Chennels.FirstOrDefault(c => c.UserId == userId);


            userProfileCount.AudioCount = _context.Audio.Count();
            userProfileCount.Subscribers = _context.Subscribes.Count();

            userProfileCount.VideoCount = (from x in _context.Videos where x.ChannelId == userChannel.ChennelId select x.Id).Count();

            //userProfileCount.VideoCount = (from v in _context.Videos where v.ChannelId == 3 select v.Id).Count();

            return Json(new { result = userProfileCount });
        }

        public IActionResult History()
        {
            return PartialView("~/Views/Shared/History/_history.cshtml");
        }

        public IActionResult Hosting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InduvialVideoPostedList()
        {

            //HttpContext.Session.GetString("UserId")
            string userIdString = HttpContext.Session.GetString("UserId");

            int userId = Convert.ToInt32(userIdString);

            var userChannel = _context.Chennels.FirstOrDefault(c => c.UserId == userId);
            //    var ChennelId = Convert.ToInt32((HttpContext.Session.GetInt32("GetChennelId")));
            var storeList = (from x in _context.Videos where x.ChannelId == userChannel.ChennelId select new { title = x.VideoTitle, description = x.Description, category = x.Category, imageType = x.ImageType, imageData = x.ImageData }).ToList();
            //var selectedVideos = _context.Videos.Where(x=> x.ChannelId ==1 ).ToList();
            return Json(new { message = storeList });
        }


        [HttpGet]
        public IActionResult GetCountry()
        {
            return Json(_context.Country.ToList());
            //  return Ok(_context.Users.Include(x => x.Id).Where(y => y.CountryId ));
        }


        [HttpGet]
        public IActionResult CityGet(int? stateId)
        {

            var storeStates = _context.State
                        .Where(x => x.CountryId == stateId).Select(x => new { countryId = x.Id, stateName = x.StateName })
                        .ToList();
            return Json(storeStates);
        }


        [HttpGet]
        public IActionResult GetStateList(int? cityId)
        {
            var listOfCity = _context.Cities
                .Where(x => x.stateId == cityId).Select(x => new { id = x.Id, cityName = x.CityName }).ToList();

            return Json(listOfCity);
        }
        [HttpGet]
        public IActionResult GetProfileInfo()
        {

            var currentUserInfoSession = HttpContext.Session.GetString("UserId");
            var currentUserInfoContext = _context.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(currentUserInfoSession));

            return Json(new { message = currentUserInfoContext });
        }

        [HttpGet]
        public IActionResult ProfileUpdate()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProfileInfo()
        {

            var loggedInUser = HttpContext.Session.GetString("UserId");
            var currentModel = _context.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(loggedInUser));
            return Json(new { stateModel = currentModel });
        }

        [HttpPut]
        [Route("Account/ProfileUpdate")]
        public IActionResult ProfileUpdate([FromBody] User model)
        {
            try
            {
                var loggedInUserId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return Json(new { success = false, message = "User not logged in" });
                }

                var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(loggedInUserId));
                if (userToUpdate != null)
                {
                    // Update the user's properties
                    userToUpdate.About = model.About;
                    userToUpdate.Categories = model.Categories;
                    userToUpdate.CountryId = model.CountryId;
                    userToUpdate.CityId = model.CityId;
                    userToUpdate.StateId = model.StateId;
                    userToUpdate.ProfileImage = model.ProfileImage;
                    userToUpdate.Name = model.Name;
                    _context.Users.Update(userToUpdate);
                    _context.SaveChanges();

                    return Json(new { success = true, message = "Profile updated successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult AddNodeModel()
        {
            var model = new Models.Notes.Notes();
            return PartialView("_AddNodeModel", model);
        }

    }
}


// 
