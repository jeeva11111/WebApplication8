using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Data;
using WebApplication8.Models.ExFile;

namespace WebApplication8.Controllers.ExFile
{
    public class ExFileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExFileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("ExFile/GetFoldersAndImages")]
        public async Task<IActionResult> GetFoldersAndImages(int? parentFolderId)
        {
            var folders = await _context.Folder
                .Where(f => f.ParentFolderId == parentFolderId)
                .ToListAsync();

            var images = await _context.Images
                .Where(i => i.FolderId == parentFolderId)
                .ToListAsync();

            return Json(new { folders, images });
        }

        [HttpPost, Route("ExFile/AddFolder")]
        public async Task<IActionResult> AddFolder(string name, int? parentFolderId)
        {
            var folder = new Folder { Name = name, ParentFolderId = parentFolderId };
            _context.Folder.Add(folder);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost, Route("ExFile/AddImage")]
        public async Task<IActionResult> AddImage([FromForm] ImageUploadModel model)
        {
            try
            {
                // Get the folderId from the model
                var folderId = model.FolderId;

                // Check if the folder exists
                var folder = await _context.Folder.FindAsync(folderId);

                if (folder == null)
                {
                    return NotFound("Folder not found");
                }

                // Handle image upload logic here
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    // Get the filename
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);

                    // Determine the path within wwwroot to save the file
                    var imagePath = Path.Combine("ExPlayer", fileName);

                    // Combine with the wwwroot path
                    var imagePathFull = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

                    // Save the file to disk
                    using (var stream = new FileStream(imagePathFull, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    // Save the image path to the database
                    var image = new Image { ImageUrl = imagePath, FolderId = folderId };
                    _context.Images.Add(image);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return BadRequest("No file uploaded.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusCode(500, "Internal server error");
            }
        }

    }

}

