using AppQuiz.Application.Chapters.Commands.Create;
using AppQuiz.Application.Chapters.Commands.Update;
using AppQuiz.Application.Models;
using AppQuiz.Application.Questions.Commands.Create;
using AppQuiz.Application.Questions.Commands.Update;
using AppQuiz.Application.Quizzes.Commands.Create;
using AppQuiz.Application.Quizzes.Commands.Update;
using AppQuiz.Domain;
using AutoMapper;
using Shared.Bus.Messages;

namespace AppQuiz.Application.Infrastructure
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<CreateChapterCommand, Chapter>();

            CreateMap<UpdateChapterCommand, Chapter>();

            CreateMap<CreateQuizCommand, Quiz>();

            CreateMap<UpdateQuizCommand, Quiz>();

            CreateMap<CreateQuestionCommand, Question>();

            CreateMap<UpdateQuestionCommand, Question>();

            CreateMap<QuizResult, QuizResultMessage>();
        }
    }
}
