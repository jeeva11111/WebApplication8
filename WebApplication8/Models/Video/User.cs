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
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Roles { get; set; }

    }
}
