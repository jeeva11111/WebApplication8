using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.Video;

namespace WebApplication8.Models.Notify
{
    public class Notify
    {
      
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool IsActive { get; set; }
        public int ChennelId { get; set; }
        public int Status { get; set; }
        [ForeignKey("UserId")]
        public User? UserList { get; set; }

        public int UserId { get; set; }
    }
}
