using System.Collections.Generic;

namespace BoardGameQuiz.Models
{
    public class QuizQuestionViewModel
    {
        public int QuestionNumber { get; set; }
        public int TotalQuestions { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string Answer { get; set; }
    }

}
