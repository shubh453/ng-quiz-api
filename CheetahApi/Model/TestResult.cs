namespace CheetahApi.Model
{
    public sealed class TestResult
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public IDictionary<int, string> MarkedAnswers { get; set; } = new Dictionary<int, string>();

        public Result ResultStatus { get; private set; } = Result.Pending;

        public Score? Score { get; set; }

        public Result MarkPassed()
        {
            return ResultStatus = Result.Passed;
        }

        public Result MarkFailed()
        {
            return ResultStatus = Result.Failed;
        }

        public void SetScore(int obtainedMarks, int totalMarks)
        {
            Score = new Score
            {
                ObtainedMarks = obtainedMarks,
                Total = totalMarks
            };

            if (totalMarks > 0 && (obtainedMarks * 100 / totalMarks) >= 33)
            {
                this.MarkPassed();
            }
            else
            {
                this.MarkFailed();
            }
        }
    }

    public enum Result
    {
        Pending,
        Passed,
        Failed
    }
}
