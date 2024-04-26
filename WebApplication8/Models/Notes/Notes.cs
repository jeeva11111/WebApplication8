using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.Video;

namespace WebApplication8.Models.Notes
{
    public class Notes
    {
        [Key]
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? ProjectTitle { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageType { get; set; }
        public byte[]? ImageData { get; set; }
        public DateTime DateTime { get; set; }
        public bool starred { get; set; } = true;

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public int UserId { get; set; }
    }

    public class NotePads
    {
        [Key]
        public int Id { get; set; }

        public string? TaskName { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? ProjectTitle { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string? ImageType { get; set; }
        public byte[]? ImageData { get; set; }
        public DateTime DateTime { get; set; }
        public bool Starred { get; set; } = true;

      
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public  User? User { get; set; }
    }

}
