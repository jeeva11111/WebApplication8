using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication8.Models.NewFolder;
namespace WebApplication8.Controllers.DropDown
{
    public class DropDownsController : Controller
    {
        public IActionResult Index()
        {
            //var store = new List<Models.NewFolder.FolderModel>() { new FolderModel() { Subfolders = new List<FileModel>() { new FileModel() { Type = "PDF", Name = "Downloads" } };
            return View();
        }
    }
}



/*
 using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class DriveController : Controller
{
    public IActionResult Index()
    {
        // Create the initial folders
        var downloads = new Folder
        {
            Name = "Downloads",
            Subfolders = new List<Folder>
            {
                new Folder
                {
                    Name = "Images",
                    Files = new List<File>
                    {
                        new File { Name = "Image1.jpg", Type = "jpg" },
                        new File { Name = "Image2.png", Type = "png" }
                    }
                },
                new Folder
                {
                    Name = "Songs",
                    Files = new List<File>
                    {
                        new File { Name = "Song1.mp3", Type = "mp3" },
                        new File { Name = "Song2.mp3", Type = "mp3" }
                    }
                },
                new Folder
                {
                    Name = "Videos",
                    Files = new List<File>
                    {
                        new File { Name = "Video1.mp4", Type = "mp4" },
                        new File { Name = "Video2.mp4", Type = "mp4" }
                    }
                }
            }
        };

        var documents = new Folder
        {
            Name = "Documents",
            Subfolders = new List<Folder>
            {
                new Folder
                {
                    Name = "Excel",
                    Files = new List<File>
                    {
                        new File { Name = "Spreadsheet.xlsx", Type = "xlsx" },
                        new File { Name = "Data.xls", Type = "xls" }
                    }
                },
                new Folder
                {
                    Name = "PDF",
                    Files = new List<File>
                    {
                        new File { Name = "Document1.pdf", Type = "pdf" },
                        new File { Name = "Document2.pdf", Type = "pdf" }
                    }
                },
                new Folder
                {
                    Name = "Text",
                    Files = new List<File>
                    {
                        new File { Name = "Note1.txt", Type = "txt" },
                        new File { Name = "Note2.txt", Type = "txt" }
                    }
                }
            }
        };

        // Add both to a list for the view
        var folders = new List<Folder> { downloads, documents };

        return View(folders);
    }
}




---------------- > model







@model List<Folder>

<h2>Drive Contents</h2>

<div id="drive">
    @foreach (var folder in Model)
    {
        <div class="folder">
            <h3>@folder.Name</h3>
            <ul>
                @foreach (var subfolder in folder.Subfolders)
                {
                    <li>
                        <span class="subfolder">@subfolder.Name</span>
                        <ul>
                            @foreach (var file in subfolder.Files)
                            {
                                <li><span class="file">@file.Name</span></li>
                            }
                        </ul>
                    </li>
                }
            </ul>
        </div>
    }
</div>

@section scripts {
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            // AJAX for loading subfolders
            $('.subfolder').click(function () {
                var subfolderName = $(this).text();
                var folderName = $(this).closest('.folder').find('h3').text();

                $.ajax({
                    url: '@Url.Action("GetSubfolder", "Drive")',
                    type: 'GET',
                    data: { folder: folderName, subfolder: subfolderName },
                    success: function (result) {
                        // Update the subfolder's contents
                        $(this).siblings('ul').html(result);
                    }.bind(this),
                    error: function (xhr, status, error) {
                        console.error(xhr.responseText);
                    }
                });
            });

            // AJAX for loading files
            $('.file').click(function () {
                var fileName = $(this).text();
                var folderName = $(this).closest('.folder').find('h3').text();

                $.ajax({
                    url: '@Url.Action("GetFileDetails", "Drive")',
                    type: 'GET',
                    data: { folder: folderName, file: fileName },
                    success: function (result) {
                        // Show file details in a modal or do something else
                        alert(result);
                    },
                    error: function (xhr, status, error) {
                        console.error(xhr.responseText);
                    }
                });
            });
        });
    </script>
}



-------------- post 

[HttpGet]
public IActionResult GetSubfolder(string folder, string subfolder)
{
    // Simulate loading subfolder content
    // You would retrieve this data from a database or actual file system
    var files = new List<File>
    {
        new File { Name = "Subfile1.txt", Type = "txt" },
        new File { Name = "Subfile2.txt", Type = "txt" }
    };

    return PartialView("_FilesPartial", files);
}

[HttpGet]
public IActionResult GetFileDetails(string folder, string file)
{
    // Simulate loading file details
    // You would retrieve this data from a database or actual file system
    var details = $"File: {file}, Folder: {folder}";
    return Content(details);
}


-- partical view 


@model List<File>

<ul>
    @foreach (var file in Model)
    {
        <li><span class="file">@file.Name</span></li>
    }
</ul>


 */