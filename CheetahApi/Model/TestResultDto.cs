namespace CheetahApi.Model
{
    public class TestResultDto
    {
        public TestResultDto(IList<string> markedAnswer, bool isPassed)
        {
            MarkedAnswer = markedAnswer;
            IsPassed = isPassed;
        }

        public IList<string> MarkedAnswer { get; set; }

        public bool IsPassed { get; set; }
    }
}
