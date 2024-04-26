using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Data;
using WebApplication8.Models.Notes;

namespace WebApplication8.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var currentUser = HttpContext.Session.GetString("UserId");

            return View(_context.NotePads.Where(x => x.UserId == Convert.ToInt32(currentUser)));
        }

        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> UploadImage(Models.Notes.NotePads model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {

                    var currentUser = HttpContext.Session.GetString("UserId");
                    model.UserId = Convert.ToInt32(currentUser);
                    using (var stream = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(stream);
                        model.ImageData = stream.ToArray();
                    }
                    model.ImageType = model.ImageFile.ContentType;
                    _context.NotePads.Update(model);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "No file selected.";
                }
            }
            return View("UploadImage", model);
        }

        [HttpGet]
        public IActionResult AddNodeModel()
        {
            var model = new Models.Notes.NotePads();
            return PartialView("_AddNodeModel", model);
        }



        [HttpPut]
        public async Task<IActionResult> EditProfile(Models.Notes.NotePads model)
        {
            if (ModelState.IsValid)
            {
                var currentModel = await _context.NotePads.FindAsync(model.Id);

                if (currentModel == null)
                {
                    return Json(new { message = " Id not found" });
                }

                currentModel.Color = model.Color;
                currentModel.DateTime = model.DateTime;
                currentModel.Description = model.Description;
                currentModel.ProjectTitle = model.ProjectTitle;
                currentModel.TaskName = model.TaskName;
                currentModel.Starred = model.Starred;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(stream);
                        currentModel.ImageData = stream.ToArray();
                    }
                    currentModel.ImageType = model.ImageFile.ContentType;
                }

                _context.NotePads.Update(currentModel);
                await _context.SaveChangesAsync();

                return Ok(currentModel);
            }

            return BadRequest(ModelState);
        }



        [HttpGet]
        public IActionResult GetNotes(int id)
        {

            if (id <= 0)
            {
                return Json(new { message = "invalid Id" });
            }

            var currentNote = _context.NotePads.Where(x => x.Id == id).FirstOrDefault();
            return Json(new { message = currentNote });
        }


        public IActionResult EditBasedOnId()
        {
            var model = new Models.Notes.NotePads();
            return PartialView("_AddNodeModel", model);
        }

    }
}
