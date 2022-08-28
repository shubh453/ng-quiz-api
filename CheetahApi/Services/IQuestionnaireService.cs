using CheetahApi.Model;

namespace CheetahApi.Services
{
    public interface IQuestionnaireService
    {
        Task<QuestionnaireDto> GetQuestionnaire(string category, int count = 10);

        Task<Questionnaire> GetQuestionnaire(int id);

        Task<QuestionnaireDto> SaveQuestionnaireResult(int questionnaireId, TestResult result);

        Task<bool> UpdateStatus(int id, QuestionnaireStatus questionnaireStatus);
    }
}
