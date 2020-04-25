using System;
using System.Collections.Generic;

namespace AppQuiz.Domain
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public ICollection<string> Options { get; set; } = new List<string>();
        public Guid QuizId { get; set; }
    }
}
