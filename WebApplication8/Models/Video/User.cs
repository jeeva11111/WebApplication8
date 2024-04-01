using System.ComponentModel.DataAnnotations;
using WebApplication8.Models.FileManager;
using WebApplication8.Models.Notify;

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

        // User has many Chennels
        public ICollection<Chennel>? Chennels { get; set; }
        public ICollection<Models.Notify.Notify> Notify { get; set; }

        public ICollection<Models.Video.Subscribes> Subscribers { get; set; }
        public ICollection<Models.Video.Audio> Audio { get; set; }

        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Roles { get; set; } = "user";

        public ICollection<Models.FileManager.FileManager> fileManagers { get; set; }

    }
}
