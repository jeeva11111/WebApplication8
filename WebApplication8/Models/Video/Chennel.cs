using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models.Video
{
    public class Chennel
    {
        [Key]
        public int ChennelId { get; set; }
        public string? ChennelName { get; set; }
        public string? ChennelDescription { get; set; }
        public string? Categorey { get; set; }

        // Channel DP 
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }

        // Channel Banner
        public byte[]? BannerData { get; set; }
        public string? BannerPath { get; set; }

        // A Channel has many Videos
        public ICollection<Video>? Videos { get; set; }
            [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }

    }
}