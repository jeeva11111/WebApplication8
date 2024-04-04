﻿using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Data;
using WebApplication8.Models.Notes;

namespace WebApplication8.Controllers.Notes
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
            return View(_context.Notes.ToList());
        }

        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(Models.Notes.Notes model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(stream);
                        model.ImageData = stream.ToArray();
                    }
                    model.ImageType = model.ImageFile.ContentType;
                    _context.Add(model);
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
            var model = new Models.Notes.Notes();
            return PartialView("_AddNodeModel", model);
        }

        public IActionResult AddNotes()
        {
            return PartialView("_HomeCaller");
        } 

    }
}
