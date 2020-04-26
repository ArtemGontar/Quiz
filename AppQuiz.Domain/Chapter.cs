using System;
using System.Collections.Generic;
using System.Text;

namespace AppQuiz.Domain
{
    public class Chapter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
    }
}
