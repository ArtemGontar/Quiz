using System;

namespace AppQuiz.Domain
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid OwnerId { get; set; }
    }
}
