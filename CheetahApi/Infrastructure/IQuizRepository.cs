using System.Linq.Expressions;
using CheetahApi.Model;

namespace CheetahApi.Infrastructure
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> Get(Expression<Func<Quiz, bool>> func);

        Task<IEnumerable<Quiz>> GetAll();

        Task<Quiz> GetById(int id);

        Task<Quiz> Save(Quiz name);

        Task Save(IEnumerable<Quiz> quizzes);

        Task<bool> DeleteById(int id);

        Task<bool> Update(int id, Quiz question);
        Task<int> DeleteAll();
    }
}
