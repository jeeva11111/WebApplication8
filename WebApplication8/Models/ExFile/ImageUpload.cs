using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.Video;

namespace WebApplication8.Models.ExFile
{
    public class ImageUpload
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
        [ForeignKey("CurrentUserId")]
        public User? User { get; set; }
        public int CurrentUserId { get; set; }
    }
}
