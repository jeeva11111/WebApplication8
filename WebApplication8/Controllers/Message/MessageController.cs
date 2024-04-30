using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using WebApplication8.Data;
using WebApplication8.Models.Message;

namespace WebApplication8.Controllers.Message
{
    public class MessageController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly RequestDelegate _requestDelegate;
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
            var currentUser = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(currentUser))
            {
                return Json(false);
            }

            var currentUserId = Convert.ToInt32(currentUser);

            var currentList = _context.Users
                                      .Where(user => user.Id != currentUserId) // Filter out the current user
                                      .Select(x => new
                                      {
                                          id = x.Id,
                                          name = x.Name,
                                          image = x.ProfileImage
                                      })
                                      .ToList();

            return Json(new { currentList });
        }

        [HttpPost]
        public IActionResult whatsApp([FromBody] Models.Message.Message data) // Assuming MessageData is a defined class with SenderId and TextMessage
        {
            var currentUser = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(currentUser))
            {
                return Json(false);
            }

            var GetEmailOfSender = _context.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(currentUser));
            if (GetEmailOfSender == null)
            {
                return Json(false);
            }

            var currentMessage = new Models.Message.Message()
            {
                SenderId = GetEmailOfSender.Id,
                ReceiverId = data.SenderId,
                TextMessage = data.TextMessage,
                TimeStamp = DateTime.Now,
                Email = GetEmailOfSender.Email,
                UserMmsId = GetEmailOfSender.Id,
                User = null

            };


            _context.Messages.Add(currentMessage);
            _context.SaveChanges();
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


        [HttpGet]
        public IActionResult GetReciverMessage(int userId)
        {

            if (userId <= 0) { return Json(new { message = "unable to find the Id" }); }
            var currentId = _context.Messages.Where(x => x.SenderId == userId);
            if (currentId == null)
            {
                return Json(new { message = "unable to find the Id" });
            }
            var selectMessage = _context.Messages.Where(x => x.ReceiverId == userId).ToList();

            return Json(new { message = currentId });
        }

        [HttpGet]
        public IActionResult GetSenderMessage()
        {

            var currentUser = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(currentUser))
            {
                return Json(false);
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == Convert.ToInt32(currentUser));
            if (user == null)
            {
                return NotFound();
            }

            var selectSendMessage = _context.Messages.Where(x => x
            .SenderId == user.Id).ToList();

            return Json(new { message = selectSendMessage });
        }

    }
}
