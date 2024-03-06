namespace WebApplication8.Models.Video
{
	public class Chennel
	{

		public int ChennelId { get; set; }
		public string? ChennelName { get; set; }
		public string? ChennelDescription { get; set; }
		public string? Categorey { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
		public byte[]? ChennelImage { get; set; }
		public ICollection<Video>? Videos { get; set; }

	}
}