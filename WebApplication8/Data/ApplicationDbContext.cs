using Microsoft.EntityFrameworkCore;
using WebApplication8.Models.Video;


namespace WebApplication8.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		//public DbSet<User> Users { get; set; }
		public DbSet<Video> Videos { get; set; }
		public DbSet<Chennel> Chennels { get; set; }
	}
}
