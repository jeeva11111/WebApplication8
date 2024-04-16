using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.Video;

namespace WebApplication8.Models.FileManager
{
    public class FileManager
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Path { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDirectory { get; set; }
        public bool HasDirectories { get; set; }
        [ForeignKey("UserId")]
        public User? user { get; set; }
        public int UserId { get; set; }
    }
}
