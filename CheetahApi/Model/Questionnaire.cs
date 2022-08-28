using CheetahApi.Extensions;

namespace CheetahApi.Model
{
    public sealed class Questionnaire
    {
        public Questionnaire(string category, IEnumerable<QuizDto> quizzes)
        {
            Quizzes = quizzes;
            Category = category;
            Results = new List<TestResult> { new() };
            AttemptCount = 0;
        }

        public Questionnaire()
        {

        }

        public int Id { get; set; }
        public string Category { get; init; }
        public IEnumerable<QuizDto> Quizzes { get; init; }
        public QuestionnaireStatus Status { get; private set; } = QuestionnaireStatus.NotTaken;
        public IList<TestResult> Results { get; set; }

        public int AttemptCount { get; private set; }

        public QuestionnaireStatus MarkStarted()
        {
            AttemptCount++;
            return Status = QuestionnaireStatus.Started;
        }

        public QuestionnaireStatus MarkCompleted()
        {
            return Status = QuestionnaireStatus.Completed;
        }

        public QuestionnaireStatus MarkRetaken()
        {
            AddNewEmptyResult();
            AttemptCount++;
            return Status = QuestionnaireStatus.Retaken;
        }

        public TestResultDto UpdateCurrentResult(TestResult result)
        {
            var currentResult = Results.LastOrDefault(r => r.ResultStatus == Result.Pending);
            if (currentResult == null)
            {
                return new TestResultDto(new Dictionary<int, string>(), null, false);
            }
            currentResult.MarkedAnswers = result.MarkedAnswers;

            var correctAnswerCount = Quizzes.Count(a => result.MarkedAnswers[a.Id] == a.Answer);
            var totalQuestions = Quizzes.Count();

            currentResult.SetScore(correctAnswerCount, totalQuestions);

            return currentResult.ToDto();
        }

        public void AddNewEmptyResult()
        {
            if (Status == QuestionnaireStatus.Completed)
            {
                throw new InvalidOperationException();
            }

            Results.Add(new TestResult());
        }
    }

    public enum QuestionnaireStatus
    {
        NotTaken,
        Started,
        Retaken,
        Completed
    }

}
