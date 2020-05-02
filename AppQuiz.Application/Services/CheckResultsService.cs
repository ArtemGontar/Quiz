using AppQuiz.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQuiz.Application.Models;

namespace AppQuiz.Application.Services
{
    public interface ICheckResultService
    {
        QuizResult CheckResult(IReadOnlyCollection<Question> questions, IList<string> answers);
    }

    public class CheckResultsService : ICheckResultService
    {
        public QuizResult CheckResult(IReadOnlyCollection<Question> questions, IList<string> answers)
        {
            var questionsCount = questions.Count();
            var correctAnswersCount = questions.Where((x, index) => x.CorrectAnswer == answers[index]).Count();
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
