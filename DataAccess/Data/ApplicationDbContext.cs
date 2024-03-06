using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;
//using WebApplication8.Models.Items;
//using WebApplication8.Models.UserContent;
//using WebApplication8.Models.VideoContent;

namespace WebApplication8.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       //// public DbSet<Register> Registers { get; set; }
      //  public DbSet<Login> Login { get; set; }
        //public DbSet<Message> Messages { get; set; }

        //// Adding course details info video makers 
        //public DbSet<VideoInfo> VideoInfo { get; set; }
        //public DbSet<User> users { get; set; }
        //public DbSet<WishList> WishList { get; set; }
    }
}
