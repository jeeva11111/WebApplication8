using Microsoft.AspNetCore.Mvc;
using WebApplication8.Data;
using WebApplication8.Models.SkillsAssignments;
using WebApplication8.Models.Video;

namespace WebApplication8.Controllers.Login
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IConfiguration _configuration;
        public AdminController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        [HttpGet]
        public IActionResult Dashboard()
        {

            var listOfUser = _context.SkillsAssignments.ToList();
            return View(listOfUser);
        }


        public IActionResult AddAssignment()
        {
            var user = new Models.SkillsAssignments.SkillsAssignmentsModel();
            return PartialView("_AddAssignment", user);
        }


        [HttpPost]
        public IActionResult AddAssignment(SkillsAssignmentsModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(currentUserId))
                {
                    int userId = Convert.ToInt32(currentUserId);
                    var assignmentModel = new SkillsAssignmentsModel()
                    {
                        Progress = model.Progress,
                        ProjectDescription = model.ProjectDescription,
                        ProjectName = model.ProjectName,
                        UserId = userId,
                        StartDate = model.StartDate,
                        Status = model.Status
                    };
                    _context.SkillsAssignments.Add(assignmentModel);
                    _context.SaveChanges();
                }
                return RedirectToAction("Dashboard");
            }
            return View(model); 
        }
    }

}

