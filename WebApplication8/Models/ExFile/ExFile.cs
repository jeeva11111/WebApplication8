using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.Video;

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
        [Key]
        public int FolderId { get; set; }
        public string? Title { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageType { get; set; }
        public byte[]? ImageData { get; set;  }
        public User? User { get; set; }
        public int UserId { get; set; }

    }
}
