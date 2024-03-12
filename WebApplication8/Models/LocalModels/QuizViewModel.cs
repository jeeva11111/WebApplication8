using WebApplication8.Models.Quiz;

namespace WebApplication8.Models.LocalModels
{
    public class QuizViewModel
    {

        public List<Quiz.Quiz>? QuizItems { get; set; }
        public List<DepOptionsList>? DepOptions { get; set; }
    }
}
