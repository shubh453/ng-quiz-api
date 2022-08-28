using System.Linq.Expressions;
using CheetahApi.Model;
using LiteDB.Async;

namespace CheetahApi.Infrastructure
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ILiteCollectionAsync<Quiz> _collection;
        private const string EnglishQuiz = "EnglishQuiz";

        public QuizRepository(ILiteDatabaseAsync database)
        {
            var db = database ?? throw new ArgumentNullException(nameof(database));
            _collection = db.GetCollection<Quiz>(EnglishQuiz);
        }

        public async Task<IEnumerable<Quiz>> Get(Expression<Func<Quiz, bool>> func)
        {
            return await _collection.Query().Where(func).ToListAsync();
        }

        public async Task<IEnumerable<Quiz>> GetAll()
        {
            return (await _collection.FindAllAsync()).ToList();
        }

        public async Task<Quiz> GetById(int id)
        {
            return await _collection.FindByIdAsync(id);
        }

        public async Task<Quiz> Save(Quiz quiz)
        {
            var result = await _collection.InsertAsync(quiz);
            quiz.Id = result.AsInt32;
            return quiz;
        }

        public async Task Save(IEnumerable<Quiz> quizzes)
        {
            await _collection.InsertAsync(quizzes);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await _collection.DeleteAsync(id);
        }

        public async Task<bool> Update(int id, Quiz question)
        {
            return await _collection.UpdateAsync(id, question);
        }

        public async Task<int> DeleteAll()
        {
            return await _collection.DeleteAllAsync();
        }
    }
}
