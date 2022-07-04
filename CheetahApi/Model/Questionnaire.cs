namespace CheetahApi.Model
{
    public sealed class Questionnaire
    {
        public Questionnaire(IEnumerable<QuizDto> quizzes)
        {
            Quizzes = quizzes;
            Result = new List<TestResult> { new() };
            AttemptCount = 0;
        }

        public int Id { get; set; }
        public IEnumerable<QuizDto> Quizzes { get; init; }
        public QuestionnaireStatus Status { get; private set; } = QuestionnaireStatus.NotTaken;
        public IList<TestResult> Result { get; }

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

        public void UpdateCurrentResult(TestResult result)
        {
            var currentResult = Result.LastOrDefault(r => r.ResultStatus == Model.Result.Pending);
            if (currentResult == null)
            {
                return;
            }

            currentResult.MarkedAnswer = result.MarkedAnswer;
            switch (result.ResultStatus)
            {
                case Model.Result.Passed:
                    currentResult.MarkPassed();
                    break;
                case Model.Result.Failed:
                    currentResult.MarkFailed();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void AddNewEmptyResult()
        {
            if (Status == QuestionnaireStatus.Completed)
            {
                throw new InvalidOperationException();
            }

            Result.Add(new TestResult());
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
