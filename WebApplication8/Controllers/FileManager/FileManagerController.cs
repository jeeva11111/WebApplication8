using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.Models.FileManager;

namespace WebApplication8.Controllers.FileManager
{

    [Route("FileManager")]
    public class FileManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileManagerController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpGet("")] // Ensures that this action is only invoked for HTTP GET at /FileManager
        public IActionResult Index()
        {
            var folders = _context.FileManager.Where(f => f.IsDirectory && f.Path == null).ToList();
            return View(folders);
        }

        [HttpPost("AddFolder")]
        public IActionResult AddFolder(string folderName, int? parentId)
        {
            var newFolder = new Models.FileManager.FileManager
            {
                Name = folderName,
                IsDirectory = true,
                Path = parentId == null ? null : _context.FileManager.Find(parentId)?.Path + "/" + folderName,
                LastModified = DateTime.Now,
                Size = 0,
                HasDirectories = false,
                UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"))
            };
            _context.FileManager.Add(newFolder);
            _context.SaveChanges();

            return Json(new
            {
                success = true,
                folderId = newFolder.Id
            });
        }

        [HttpGet("GetContents/{folderId}")]
        public IActionResult GetContents(int folderId)
        {
            var folder = _context.FileManager.Find(folderId);
            if (folder != null)
            {
                var contents = _context.FileManager.Where(f => f.Path.StartsWith(folder.Path)).ToList();
                return Json(contents);
            }
            return NotFound();
        }


        public IActionResult PostSubFolder(int parentId)
        {
            var subFolder = _context.FileManager.Find(parentId);

            return Json(subFolder);
        }

    }
}
