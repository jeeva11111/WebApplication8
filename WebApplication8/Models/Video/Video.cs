using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models.Video
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(355)]
        public string? VideoTitle { get; set; }
        [MaxLength(5355)]
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(50)]
        public string? Category { get; set; }
        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
    }
}
