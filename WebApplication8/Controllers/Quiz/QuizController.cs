using Microsoft.AspNetCore.Mvc;
using WebApplication8.Data;
using WebApplication8.Filters;
using WebApplication8.Models.LocalModels;

namespace WebApplication8.Controllers.Quiz
{


    //[CustomSessionFilter]
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var viewModel = new QuizViewModel()
            {
                DepOptions = _context.DepOptionsLists.ToList(),
                QuizItems = _context.Quiz.ToList()
            };

            return View(viewModel);
        }

        public IActionResult GetListOfQuiz()
        {
            var viewModel = new QuizViewModel()
            { QuizItems = _context.Quiz.ToList(), DepOptions = _context.DepOptionsLists.ToList() };
            return Json(viewModel);
        }

        [HttpPost]
        public IActionResult SelectDepartment(string input)
        {
            var store = _context.Quiz.Where(q => q.Department == input).ToList();
            return Json(store);
        }
    }
}
