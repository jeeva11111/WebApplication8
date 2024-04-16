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
using WebApplication8.Models.Video;
using Newtonsoft.Json;
using System.IO.Compression;
namespace WebApplication8.Controllers.ExFile
{
    [Route("ExFile")]
    public class ExFileController : Controller
    {
        private readonly ApplicationDbContext _context;

        private List<object> _storeCurrentExcel;
        public ExFileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet, Route("GetFile")]
        public IActionResult GetFile()
        {
            // Get the current user's ID from session
            var currentUser = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUser))
            {
                return BadRequest("User ID not found in session.");
            }

            // Safely try to convert the user ID to integer
            if (!int.TryParse(currentUser, out int userId))
            {
                return BadRequest("Invalid User ID in session.");
            }

            try
            {
                // Perform the query using a proper join
                var message = (from user in _context.Users
                               where user.Id == userId
                               join file in _context.ImageUploads on user.Id equals file.CurrentUserId
                               select  file ).ToList();

                return Json(message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error. Please try again later. {ex.Message}");
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
                        var excelDataItems = new List<DynamicExcelData>();
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



        [HttpGet, Route("GettingExcelData")]
        public IActionResult GettingExcelData()
        {
            return Json(new { message = _storeCurrentExcel });
        }

        [HttpGet, Route("GetExcelData")]
        public IActionResult GetExcelData()
        {
            try
            {
                var excelData = _context.ExcelData
                    .Select(d => new
                    {
                        d.Id,
                        Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(d.DataJson),
                        d.CreatedAt
                    }).ToList();

                return Ok(excelData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data: " + ex.Message);
            }
        }

        [HttpPost, Route("AddFile")]
        public async Task<IActionResult> AddFile(IFormFile file)
        {

            // Get the current user's ID from session
            var currentUser = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUser))
            {
                return BadRequest("User ID not found in session.");
            }

            // Safely try to convert the user ID to integer
            if (!int.TryParse(currentUser, out int userId))
            {
                return BadRequest("Invalid User ID in session.");
            }


            if (file != null && file.Length > 0)
            {
                var image = new ImageUpload()
                {
                    FileName = file.FileName,
                    FileData = new byte[file.Length],
                    CurrentUserId = userId
                };
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    image.FileData = stream.ToArray();
                }
                _context.ImageUploads.Add(image);
                _context.SaveChanges();
            }
            return View();
        }


        [HttpPost, Route("DeleteFile/{id}")]
        public IActionResult DeleteTheFile(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var currentModel = _context.ImageUploads.FirstOrDefault(x => x.Id == id);
            if (currentModel == null)
            {
                return NotFound();
            }

            _context.ImageUploads.Remove(currentModel);
            _context.SaveChanges();

            return Json(new { status = true });
        }


        [HttpGet, Route("DownloadFile/{id}")]
        public IActionResult DownloadFile(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var file = _context.ImageUploads.FirstOrDefault(x => x.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            var stream = new MemoryStream(file.FileData);
            string contentType;
            if (file.FileName.EndsWith(".xlsx"))
            {
                contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }

            else if (file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                contentType = "application/pdf";
            }

            else if (file.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || file.FileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                contentType = "image/jpeg";
            }

            else
            {
                contentType = "application/octet-stream";
            }

            return File(stream, contentType, file.FileName);
        }




    }
}

