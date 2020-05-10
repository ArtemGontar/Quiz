using AppQuiz.Application.Chapters.Commands.Create;
using AppQuiz.Application.Chapters.Commands.Delete;
using AppQuiz.Application.Chapters.Commands.Update;
using AppQuiz.Application.Models;
using AppQuiz.Application.Questions.Commands.Create;
using AppQuiz.Application.Questions.Commands.Update;
using AppQuiz.Application.Quizzes.Commands.Create;
using AppQuiz.Application.Quizzes.Commands.Delete;
using AppQuiz.Application.Quizzes.Commands.Update;
using AppQuiz.Domain;
using AutoMapper;
using Shared.Bus.Messages;
using Shared.Bus.Messages.Messages;

namespace AppQuiz.Application.Infrastructure
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            // to map public and internal properties
            ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

            CreateMap<CreateChapterCommand, Chapter>();

            CreateMap<UpdateChapterCommand, Chapter>();

            CreateMap<CreateQuizCommand, Quiz>();

            CreateMap<UpdateQuizCommand, Quiz>();

            CreateMap<CreateQuestionCommand, Question>();

            CreateMap<Question, CreateQuestionCommand>();

            CreateMap<UpdateQuestionCommand, Question>();

            CreateMap<Question, UpdateQuestionCommand>();

            CreateMap<QuizResult, QuizResultMessage>();

            CreateMap<DeleteChapterCommand, DeleteChapterMessage>();

            CreateMap<DeleteQuizCommand, DeleteQuizMessage>();

        }
    }
}
