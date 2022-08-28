using CheetahApi.Model;

namespace CheetahApi.Extensions
{
    public static class QuestionnaireConversionExtension
    {
        public static QuestionnaireDto ToDto(this Questionnaire questionnaire)
        {
            var results = questionnaire.Quizzes.ToDictionary(q => q.Id, q => string.Empty);
            return new QuestionnaireDto(
                questionnaire.Id,
                questionnaire.Category,
                questionnaire.Quizzes,
                new TestResultDto(results, null ,false));
        }
    }
}
