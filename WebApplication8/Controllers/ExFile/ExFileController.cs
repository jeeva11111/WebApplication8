using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ExcelDataReader.Exceptions;
using WebApplication8.Data;
using WebApplication8.Models.ExFile;

using ExcelDataReader.Core;
using System.Text;
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

        [HttpPost]
        [Route("ExFile/AddFile")]
        public async Task<IActionResult> AddFile([FromForm] ImageUploadModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("File", "Please select a file ");
                return Json(new { message = "Model data is not valid" });
            }
            return Json(new { success = false, message = "No file uploaded." });
        }

        [HttpGet]
        public IActionResult AddTaskDrive()
        {
            var listOfTaskDrive = _context.ImageFile.ToList();
            return PartialView("_AddTaskModel", listOfTaskDrive);
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

        [HttpPost]
        public IActionResult ExcelFileReader()
        {
            try
            {
                List<List<object>> excelData = new List<List<object>>();

                // Reading the Excel file from the request
                using (var stream = new MemoryStream())
                {
                    var file = Request.Form.Files["file"];
                    file.CopyTo(stream);

                    // Specify UTF-8 encoding
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = Encoding.GetEncoding(1252)

                    }))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                var rowData = new List<object>();
                                for (int column = 0; column < reader.FieldCount; column++)
                                {
                                    rowData.Add(reader.GetValue(column));
                                }
                                excelData.Add(rowData);
                            }
                        } while (reader.NextResult());
                    }
                }
                // Return JSON data
                return Json(new { excelData = excelData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error processing Excel file: " + ex.Message);
            }
        }
    }
}


