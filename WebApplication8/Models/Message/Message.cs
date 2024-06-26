﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication8.Models.Video;

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
        [ForeignKey("UserMmsId")]
        public User? User { get; set; }
        
        public int UserMmsId { get; set; }
        public string? Email { get; set; }
    }
}
