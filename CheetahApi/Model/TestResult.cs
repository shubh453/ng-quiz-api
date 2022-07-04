namespace CheetahApi.Model
{
    public sealed class TestResult
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public IList<string> MarkedAnswer { get; set; } = new List<string>();

        public Result ResultStatus { get; private set; } = Result.Pending;


        public Result MarkPassed()
        {
            return ResultStatus = Result.Passed;
        }

        public Result MarkFailed()
        {
            return ResultStatus = Result.Failed;
        }
    }

    public enum Result
    {
        Pending,
        Passed,
        Failed
    }
}
