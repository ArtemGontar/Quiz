using System;
using System.Collections.Generic;

namespace AppQuiz.Domain
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CorrectAnswer { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();
        public Guid QuizId { get; set; }
    }
}
