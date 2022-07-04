using CheetahApi.Model;

namespace CheetahApi.Extensions
{
    public static class QuestionnaireConversionExtension
    {
        public static QuestionnaireDto ToDto(this Questionnaire questionnaire)
        {
            return new QuestionnaireDto(questionnaire.Id, "", questionnaire.Quizzes);
        }
    }
}
