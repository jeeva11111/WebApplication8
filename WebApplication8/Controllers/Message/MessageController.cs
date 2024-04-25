using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using WebApplication8.Data;
using WebApplication8.Models.Message;

namespace WebApplication8.Controllers.Message
{
    public class MessageController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        // [Route("sendMessage")]
        public IActionResult SendMessage(string message, int senderId)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(currentUser))
            {
                return Json(false);
            }

            var messageEntity = new Models.Message.Message()
            {
                SenderId = Convert.ToInt32(currentUser),
                ReceiverId = senderId,
                TextMessage = message,
                TimeStamp = DateTime.Now
            };

            _context.Messages.Add(messageEntity);
            _context.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var currentList = _context.Users.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                image = x.ProfileImage
            }).ToList();


            var selector = (from x in _context.Users join m in _context.Messages on x.Id equals m.UserMmsId select new { name = x.Name, image = x.ProfileImage, m.TimeStamp }).ToList();
            return Json(new { currentList });
        }

        [HttpPost]
        public IActionResult whatsApp(int senderId, string textMessage)
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(currentUser))
            {
                return Json(false);
            }

            var GetEmailOfSender = _context.Users.Where(x => x.Id == Convert.ToInt32(currentUser)).First();

            var currentMessage = new Models.Message.Message()
            {
                SenderId = Convert.ToInt32(currentUser),
                ReceiverId = senderId,
                TextMessage = textMessage,
                TimeStamp = DateTime.Now,
                Email = GetEmailOfSender.Email
            };

            return Json(new { message = currentMessage });
        }



        [HttpGet]
        public IActionResult GetUserInfo(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound(); // Return 404 if user not found
            }

            // Map user data to a simple object for JSON response
            var userInfo = new
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                // Add other properties you want to include
            };

            return Json(userInfo);
        }



    }
}
