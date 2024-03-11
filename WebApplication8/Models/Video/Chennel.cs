using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models.Video
{
    public class Chennel
    {
        [Key]
        public int ChennelId { get; set; }
        public string? ChennelName { get; set; }
        public string? ChennelDescription { get; set; }
        public string? Categorey { get; set; }
        public string? ImagePath { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }
        public ICollection<Video>? Videos { get; set; }

    }
}