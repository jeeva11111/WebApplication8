using Microsoft.AspNetCore.Mvc;
using WebApplication8.Data;
using WebApplication8.Models.Video;
using WebApplication8.Filters;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication8.Models.Notify;
using System.Configuration;
using Microsoft.AspNetCore.Http;
namespace WebApplication8.Controllers
{
    [CustomSessionFilter]
    public class VideoController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _environment;

        public VideoController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        [HttpGet]
        public IActionResult GetListOfVideos()
        {

            return Json(_context.Videos.ToList());
        }

        [HttpGet]

        public IActionResult CreateVideo()
        {
            return View();
        }

        [HttpPost]
        [NotificationFilter]
        public async Task<IActionResult> CreateVideo(Video video)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Items["VideoTitle"] = video.VideoTitle;
                HttpContext.Items["VideoDescription"] = video.Description;

                HttpContext.Session.SetInt32("GetChennelId", video.ChannelId);

                if (video.ImageFile != null && video.ImageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await video.ImageFile.CopyToAsync(stream);
                        video.ImageData = stream.ToArray();
                    }
                    video.ImageType = video.ImageFile.ContentType;
                }

                // Upload Video
                if (video.VideoFile != null && video.VideoFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await video.VideoFile.CopyToAsync(stream);
                        video.VideoData = stream.ToArray();
                    }
                    video.VideoType = video.VideoFile.ContentType;
                }

                video.CreatedDate = DateTime.Now;

                int channelId = GetChannelIdForCurrentUser();
                HttpContext.Items["ChennelId"] = channelId;
                var channel  = _context.Chennels.FirstOrDefault(c => c.ChennelId == channelId);
                if (channel == null)
                {
                    return RedirectToAction("Error", "Home");
                }

                video.ChannelId = channelId;

                _context.Add(video);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Chennel");
            }

            return View(video);
        }

        // Getting the video Notifaction message after posting the video
        [HttpGet]
        public JsonResult GetAllNotify()
        {
            var selectNotify = _context.Notifys.ToList();
            return Json(new { selectedNotify = selectNotify.LastOrDefault() });
        }

        private int GetChannelIdForCurrentUser()
        {
            string userIdString = HttpContext.Session.GetString("UserId");

            int userId = Convert.ToInt32(userIdString);

            var userChannel = _context.Chennels.FirstOrDefault(c => c.UserId == userId);

            return userChannel?.ChennelId ?? 0;
        }

    }
}
