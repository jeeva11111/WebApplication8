using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models.Video
{
    public class Audio
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile ModelFile { get; set; }
        public string FileName { get; set; }
        public int ChannelId { get; set; }

        [ForeignKey("ChannelId")]
        public Chennel? Channel { get; set; }
    }
}
