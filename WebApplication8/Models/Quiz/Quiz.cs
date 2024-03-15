using System.ComponentModel.DataAnnotations;
using WebApplication8.Models.Video;

namespace WebApplication8.Models.Quiz
{

    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Title { get; set; }
        public bool IsCorrect { get; set; }
        public string? Answer { get; set; }
        public User? User { get; set; }
        public string? Department { get; set; }
        public List<DepOptionsList>? DepOptionsLists { get; set; }
    }

    public class DepOptionsList
    {
        [Key]
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string? Title { get; set; }
        public Quiz? Quiz { get; set; }

    }

  
}
