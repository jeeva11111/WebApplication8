using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication8.Data;
using WebApplication8.Models.Video;
using System.IO;

using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
namespace WebApplication8.Controllers
{
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

            return View(_context.Videos.ToList());
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

                // Add to database
                _context.Add(video);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(video);
        }
    }
    
}
