using System.ComponentModel.DataAnnotations;
using WebApplication8.Models.Video;

namespace WebApplication8.Models.Admin
{
    public class AdminTeams
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Categoery { get; set; }
        public bool IsActive { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
