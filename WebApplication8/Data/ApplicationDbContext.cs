using Microsoft.EntityFrameworkCore;
using WebApplication8.Models.Quiz;
using WebApplication8.Models.Video;


namespace WebApplication8.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Chennel> Chennels { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<DepOptionsList> DepOptionsLists { get; set; }
    }
}
