using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models.Message
{
    public class Message
    {

        [Key]
        public int Id { get; set; }
        public string? TextMessage { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
