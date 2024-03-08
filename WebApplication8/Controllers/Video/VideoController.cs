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
        public async Task<IActionResult> CreateVideo(Video video)
        {

            if (ModelState.IsValid)
            {
                if (video.ImageFile != null && video.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await video.ImageFile.CopyToAsync(memoryStream);
                        video.ImageData = memoryStream.ToArray();
                        video.ImageType = video.ImageFile.ContentType;
                    }
                }

                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetListOfVideos));
            }


            return View(video);
        }



    }
}
