using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using WebApplication8.Data;
using WebApplication8.Models;
using WebApplication8.Models.FileManager;

namespace WebApplication8.Controllers.FileManager
{
    public class FileManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private const string RootFolderPath = "Files"; // Updated root folder path

        public FileManagerController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Get the list of files and folders
        public ActionResult GetFileList()
        {
            var rootPath = Path.Combine(_hostingEnvironment.WebRootPath, RootFolderPath);

            var files = Directory.GetFiles(rootPath)
                                  .Select(f => new FileInfo(f))
                                  .Select(f => new Models.FileManager.FileManager
                                  {
                                      Name = f.Name,
                                      Path = f.FullName,
                                      Size = f.Length,
                                      LastModified = f.LastWriteTime,
                                      IsDirectory = false,
                                      HasDirectories = false // For demonstration, adjust as needed
                                  })
                                  .ToList();

            var directories = Directory.GetDirectories(rootPath)
                                       .Select(d => new DirectoryInfo(d))
                                       .Select(d => new Models.FileManager.FileManager
                                       {
                                           Name = d.Name,
                                           Path = d.FullName,
                                           IsDirectory = true,
                                           HasDirectories = Directory.GetDirectories(d.FullName).Any(),
                                           // For demonstration, we're not including size and last modified for directories
                                       })
                                       .ToList();

            var allItems = files.Concat(directories).ToList();
            return Json(allItems);
        }

        // Example method to save a file to database
        public IActionResult SaveFileToDatabase(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            var fileModel = new Models.FileManager.FileManager
            {
                Name = fileInfo.Name,
                Path = fileInfo.FullName,
                Size = fileInfo.Length,
                LastModified = fileInfo.LastWriteTime,
                IsDirectory = false,
                HasDirectories = false
            };

            _context.fileManagers.Add(fileModel);
            _context.SaveChanges();

            return Ok("File saved to database.");
        }

        // Upload file
        [HttpPost, Route("FileManager/Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            // Create a unique file name to prevent overwriting
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, RootFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save file information to the database
            SaveFileToDatabase(filePath);

            return Ok("File uploaded successfully.");
        }

        // Create folder
        [HttpPost, Route("FileManager/CreateFolder")]
        public IActionResult CreateFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return BadRequest("Folder name is required.");
            }

            var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, RootFolderPath, folderName);

            if (Directory.Exists(folderPath))
            {
                return Conflict("Folder already exists.");
            }

            Directory.CreateDirectory(folderPath);

            return Ok("Folder created successfully.");
        }

        // Delete selected file or folder
        [HttpPost, Route("FileManager/DeleteSelected")]
        public IActionResult DeleteSelected([FromBody] string[] paths)
        {
            if (paths == null || paths.Length == 0)
            {
                return BadRequest("Paths are required.");
            }

            try
            {
                foreach (var path in paths)
                {
                    var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, RootFolderPath, path.TrimStart('/'));

                    if (!System.IO.File.Exists(fullPath) && !Directory.Exists(fullPath))
                    {
                        return NotFound($"File or folder not found: {path}");
                    }

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    else if (Directory.Exists(fullPath))
                    {
                        Directory.Delete(fullPath, true); // Delete recursively
                    }
                }

                return Ok("Files or folders deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
