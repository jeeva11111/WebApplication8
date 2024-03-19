using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication8.Data;
using WebApplication8.Models.Video;
using System.IO;

using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using WebApplication8.Filters;
using System.Threading.Channels;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVideo(Video video)
        {
            if (ModelState.IsValid)
            {
                // Upload Image
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

                // Set Created Date
                video.CreatedDate = DateTime.Now;

                // Get the ChannelId from the currently logged-in user or some other source
                int channelId = GetChannelIdForCurrentUser(); // Implement this method to get ChannelId

                // Check if the channel with the provided channelId exists
                var channel = _context.Chennels.FirstOrDefault(c => c.ChennelId == channelId);
                if (channel == null)
                {
                    // Channel with the provided channelId does not exist
                    // Handle this case, perhaps return an error or redirect
                    return RedirectToAction("Error", "Home");
                }

                // Assign the channelId to the video
                video.ChannelId = channelId;

                // Add to database
                _context.Add(video);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Chennel");
            }

            return View(video);
        }


        private int GetChannelIdForCurrentUser()
        {
            // Example: Retrieve UserId from session
            string userIdString = HttpContext.Session.GetString("UserId");

            // Convert UserId to integer (you should handle conversion/validation)
            int userId = Convert.ToInt32(userIdString);

            // Query the database for the user's channel
            var userChannel = _context.Chennels.FirstOrDefault(c => c.UserId == userId);

            // Return ChannelId if userChannel is found, otherwise return a default value or handle as needed
            return userChannel?.ChennelId ?? 0; // Return 0 or some default value if userChannel is null
        }


    }

}
