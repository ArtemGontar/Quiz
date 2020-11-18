using System;
using System.Collections.Generic;
using System.Text;
using Shared.Common;

namespace AppQuiz.Domain
{
    public class Chapter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        public Guid OwnerId { get; set; }
    }
}
