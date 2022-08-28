namespace CheetahApi.Model
{
    public class QuestionnaireDto
    {
        public QuestionnaireDto(int id, string category, IEnumerable<QuizDto> quizzes, TestResultDto testResult)
        {
            Id = id;
            Category = category;
            Quizzes = quizzes;
            TestResult = testResult;
        }

        public QuestionnaireDto() { }

        public int Id { get; set; }

        public string Category { get; set; }

        public IEnumerable<QuizDto> Quizzes { get; set; }

        public TestResultDto TestResult { get; set; }
    }
}
