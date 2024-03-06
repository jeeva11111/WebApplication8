using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models.Video
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? About { get; set; }
        public string? Categories { get; set; }
        public byte[]? ProfileImage { get; set; }
        public ICollection<Chennel>? Chennels { get; set; }
    }
}
