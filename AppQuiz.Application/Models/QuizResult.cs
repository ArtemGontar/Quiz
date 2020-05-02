using System;
using System.Collections.Generic;
using System.Text;

namespace AppQuiz.Application.Models
{
    public class QuizResult
    {
        public int QuestionsCount { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int WrongAnswersCount { get; set; }
        public double CorrectPercent { get; set; }

    }
}
