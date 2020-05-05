using System;
using Shared.Common;

namespace AppQuiz.Domain
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public Guid ChapterId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
