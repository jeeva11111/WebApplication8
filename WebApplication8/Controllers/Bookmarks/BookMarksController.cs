﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace WebApplication8.Controllers.Bookmarks
{
    public class BookMarksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MailServices()
        {
            return PartialView("~/Views/Shared/History_MailServices.cshtml");
        }

        [HttpGet]
        public IActionResult ShowBookMarks()
        {
            return PartialView("_BooksMarks");
        }


    }
}
