using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models.Video
{
    public class Subscribes
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int UserId { get; set; }

        [ForeignKey("ChennelId")]
        public Chennel? Chennel { get; set; }

        public int ChennelId { get; set; }
    }
}
