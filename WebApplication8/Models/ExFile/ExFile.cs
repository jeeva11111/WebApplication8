using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models.ExFile
{
    public class ExFile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        public int FolderId { get; set; }
        public Folder? Folder { get; set; }
    }

    public class Folder
    {
        public int FolderId { get; set; }
        public string? Name { get; set; }
        public int? ParentFolderId { get; set; }
        public ICollection<ExFile>? ExFiles { get; set; }
        public ICollection<Folder>? SubFolder { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int FolderId { get; set; }
        [ForeignKey("FolderId")]
        public Folder Folder { get; set; }
    }
    public class ImageUploadModel
    {
        public int FolderId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
