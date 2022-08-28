using CheetahApi.Model;
using LiteDB.Async;
using System.Linq.Expressions;

namespace CheetahApi.Infrastructure
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly ILiteCollectionAsync<Questionnaire> _collection;
        private const string EnglishQuestionnaire = "EnglishQuestionnaire";

        public QuestionnaireRepository(ILiteDatabaseAsync database)
        {
            var db = database ?? throw new ArgumentNullException(nameof(database));
            _collection = db.GetCollection<Questionnaire>(EnglishQuestionnaire);
        }

        public async Task<IEnumerable<Questionnaire>> Get(Expression<Func<Questionnaire, bool>> func)
        {
            return await _collection.Query().Where(func).ToListAsync();
        }

        public async Task<IEnumerable<Questionnaire>> GetAll()
        {
            return (await _collection.FindAllAsync()).ToList();
        }

        public async Task<Questionnaire> GetById(int id)
        {
            return await _collection.FindOneAsync(q => q.Id == id);
        }

        public async Task<Questionnaire> Save(Questionnaire questionnaire)
        {
            var result = await _collection.InsertAsync(questionnaire);
            questionnaire.Id = result.AsInt32;
            return questionnaire;
        }

        public async Task Save(IEnumerable<Questionnaire> questionnaires)
        {
            await _collection.InsertAsync(questionnaires);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await _collection.DeleteAsync(id);
        }

        public async Task<bool> Update(int id, Questionnaire questionnaire)
        {
            return await _collection.UpdateAsync(id, questionnaire);
        }

        public async Task<int> DeleteAll()
        {
            return await _collection.DeleteAllAsync();
        }
    }
}
