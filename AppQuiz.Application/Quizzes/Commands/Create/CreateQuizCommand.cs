﻿using System;
using System.Collections.Generic;
using AppQuiz.Domain;
using MediatR;
using Shared.Common;

namespace AppQuiz.Application.Quizzes.Commands.Create
{
    public class CreateQuizCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public int Priority { get; set; }
        public IEnumerable<Question> Questions { get; set; } = new List<Question>();
        public Guid ChapterId { get; set; }
        public Guid OwnerId { get; set; }
    }
}
