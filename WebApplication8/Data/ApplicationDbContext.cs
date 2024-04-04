using Microsoft.EntityFrameworkCore;
using WebApplication8.Models.Account.Profile;
using WebApplication8.Models.ExFile;
using WebApplication8.Models.FileManager;
using WebApplication8.Models.Notes;
using WebApplication8.Models.Notify;
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
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Notify> Notifys { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Subscribes> Subscribes { get; set; }
        public DbSet<Audio> Audio { get; set; }
        public DbSet<FileManager> fileManagers { get; set; }

        public DbSet<Folder> Folder { get; set; }
        public DbSet<ExFile> ExFiles { get; set; }
        public DbSet<Models.ExFile.Image> Images { get; set; }

        public DbSet<ImageUploadModel> ImageFile { get; set; }
    }
}
