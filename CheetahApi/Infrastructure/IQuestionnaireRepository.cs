using CheetahApi.Model;
using System.Linq.Expressions;

namespace CheetahApi.Infrastructure
{
    public interface IQuestionnaireRepository
    {
        Task<IEnumerable<Questionnaire>> Get(Expression<Func<Questionnaire, bool>> func);

        Task<IEnumerable<Questionnaire>> GetAll();

        Task<Questionnaire> GetById(int id);

        Task<Questionnaire> Save(Questionnaire name);

        Task Save(IEnumerable<Questionnaire> questionnaires);

        Task<bool> DeleteById(int id);

        Task<bool> Update(int id, Questionnaire questionnaire);

        Task<int> DeleteAll();
    }
}
