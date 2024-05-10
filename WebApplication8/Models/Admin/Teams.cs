using WebApplication8.Models.Video;

namespace WebApplication8.Models.Admin
{
    public class Teams
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public string? Name { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }

    }
}
