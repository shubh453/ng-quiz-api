namespace CheetahApi.Model
{
    public class TestResultDto
    {
        public TestResultDto()
        {

        }

        public TestResultDto(IDictionary<int, string> markedAnswer, Score? score, bool isPassed)
        {
            MarkedAnswer = markedAnswer;
            Score = score;
            IsPassed = isPassed;
        }

        public IDictionary<int, string> MarkedAnswer { get; set; }

        public Score? Score { get; set; }

        public bool IsPassed { get; set; }
    }
}
