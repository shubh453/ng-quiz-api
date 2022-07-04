using CheetahApi.Model;

namespace CheetahApi.Services
{
    public interface IQuestionnaireService
    {
        Task<QuestionnaireDto> GetQuestionnaire(string category, int count = 10);

        Task<bool> SaveQuestionnaireResult(int questionnaireId, TestResult result);

        Task<QuestionnaireStatus> UpdateStatus(int id, QuestionnaireStatus questionnaireStatus);
    }
}
