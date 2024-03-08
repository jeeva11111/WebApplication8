using Microsoft.AspNetCore.Mvc;
using WebApplication8.Data;

namespace WebApplication8.Controllers.Chennel
{
    public class ChennelController : Controller
    {
        private ApplicationDbContext _context;
        public ChennelController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetVideos()
        {
            return Json(_context.Videos.ToList());
        }

        public IActionResult GetChennel()
        {
            return Json(_context.Chennels.ToList());
        }
        public IActionResult AddChennel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddChennel(Models.Video.Chennel chennel, IFormFile imageFile)
        {

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        chennel.ImageData = memoryStream.ToArray();
                        chennel.ImageType = imageFile.ContentType;
                        //chennel.ImagePath = memoryStream.ToString();
                    }
                }
                _context.Chennels.Add(chennel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(chennel);
        }

        // Getting the single page video;
        [HttpGet]
        public IActionResult GetSingleVideo(int id)
        {
            var occuredVideo = _context.Videos.Where(x => x.Id == id).FirstOrDefault();
            if (occuredVideo != null)
            {
                
                ViewBag.video = id;
            }
          
            ViewBag.viewData = "Hello world";
            return View(occuredVideo);
        }

    }
}
