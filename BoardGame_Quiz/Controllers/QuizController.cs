using BoardGame_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BoardGame_Quiz.Controllers
{
    public class QuizController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult QuizQuestion(string difficulty)
        {
            // Load questions based on difficulty
            //var questions = GetQuestionsByDifficulty(difficulty);

            // Mock data for the question
            var model = new QuizQuestionViewModel
            {
                QuestionNumber = 1,
                TotalQuestions = 5,
                Question = "Which of these was NOT an event from past Olympic Games?",
                Options = new List<string>
                            {
                                "a) Netball",
                                "b) Live pigeon shooting",
                                "c) Motor boating",
                                "d) Underwater swimming"
                            },
                Answer = "Netball"
            };
            return View(model);
        }

        //private List<Question> GetQuestionsByDifficulty(string difficulty)
        //{
        //    // Mock data based on difficulty
        //    var easyQuestions = new List<Question> { /* ... */ };
        //    var mediumQuestions = new List<Question> { /* ... */ };
        //    var hardQuestions = new List<Question> { /* ... */ };

        //    return difficulty switch
        //    {
        //        "easy" => easyQuestions,
        //        "medium" => mediumQuestions,
        //        "hard" => hardQuestions,
        //        _ => easyQuestions
        //    };
        //}

    }
}
