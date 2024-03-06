using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication8.Data;
using WebApplication8.Models.Video;

namespace WebApplication8.Controllers
{
    public class VideoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Video/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Video/Create POST: Video/Create
        [HttpPost]

        public IActionResult Create(Video video, IFormFile imageFile)
        {
            video.CreatedDate = DateTime.Now;

            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    imageFile.CopyTo(memoryStream);
                    video.Image = memoryStream.ToArray();
                }
                _context.Videos.Add(video);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: Video/Index
        public IActionResult Index()
        {
            var videos = _context.Videos.ToList();
            return View(videos);
        }
    }
}
