using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.Video;

namespace WebApplication8.Models.ExFile
{
    public class DynamicExcelData
    {
        [Key]
        public int Id { get; set; }
        public string? DataJson { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
