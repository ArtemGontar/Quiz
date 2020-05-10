using AppQuiz.Application.Models;
using AppQuiz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppQuiz.Application.Services
{
    public interface ICheckResultService
    {
        QuizResult CheckResult(IReadOnlyCollection<string> correctAnswers, IList<string> answers);
    }

    public class CheckResultsService : ICheckResultService
    {
        public QuizResult CheckResult(IReadOnlyCollection<string> correctAnswers, IList<string> answers)
        {
            if(correctAnswers == null)
            {
                throw new ArgumentNullException(nameof(correctAnswers));
            }
            if (answers == null)
            {
                throw new ArgumentNullException(nameof(answers));
            }

            if(correctAnswers.Count != answers.Count)
            {
                throw new InvalidOperationException("Different count questions and answers");
            }

            var questionsCount = correctAnswers.Count();
            var correctAnswersCount = correctAnswers.Where((x, index) => x == answers[index]).Count();
            var wrongAnswersCount = questionsCount - correctAnswersCount;
            var correctPercent = (double)correctAnswersCount / questionsCount;
            return new QuizResult()
            {
                QuestionsCount = questionsCount,
                CorrectAnswersCount = correctAnswersCount,
                WrongAnswersCount = wrongAnswersCount,
                CorrectPercent = correctPercent
            };
        }
    }
}
