using CheetahApi.Model;

namespace CheetahApi.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetAllQuestions();

        Task<Quiz> GetQuestionById(int id);

        Task<Quiz> SaveQuestion(Quiz name);

        Task<IEnumerable<Quiz>> SaveQuestions(IEnumerable<Quiz> quizzes);

        Task<bool> DeleteQuestionById(int id);

        Task<bool> UpdateQuestion(int id, Quiz question);

        Task<int> DeleteAllQuestions();
    }
}