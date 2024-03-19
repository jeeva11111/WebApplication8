using Microsoft.AspNetCore.Mvc;
using WebApplication8.Data;
using WebApplication8.Models.Video;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace WebApplication8.Controllers
{
    [CustomSessionFilter]
    public class ChennelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChennelController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<Chennel>? channelsWithVideos = _context.Chennels.Include(x=> x.Videos).ToList();
            return View(channelsWithVideos);
           
        }


        public IActionResult GetVideos()
        {
            return Json(_context.Videos.ToList());
        }

        public IActionResult GetChennel()
        {
            return Json(_context.Chennels.ToList());
        }

        [HttpGet]
        public IActionResult AddChennel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddChennel(Chennel chennel, IFormFile imageData, IFormFile bannerData)
        {
            if (ModelState.IsValid)
            {
                if (imageData != null && imageData.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageData.CopyToAsync(memoryStream);
                        chennel.ImageData = memoryStream.ToArray();
                        chennel.ImageType = imageData.ContentType;
                    }
                }

                if (bannerData != null && bannerData.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await bannerData.CopyToAsync(memoryStream);
                        chennel.BannerData = memoryStream.ToArray();
                        chennel.BannerPath = bannerData.ContentType;
                    }
                }
                chennel.UserId =  Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                _context.Chennels.Add(chennel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(chennel);
        }

        [HttpGet]
        public IActionResult GetSingleVideo(int id)
        {
            var occurredVideo = _context.Videos.FirstOrDefault(x => x.Id == id);

            if (occurredVideo == null)
            {
                return NotFound();
            }

            return View(occurredVideo);
        }

        [HttpGet]
        public IActionResult GetSingleChannel(int id)
        {
            var occurredChannel = _context.Chennels.FirstOrDefault(x => x.ChennelId == id);

            if (occurredChannel == null)
            {
                return NotFound();
            }

            return View(occurredChannel);
        }
    }
}
