using CheetahApi.Model;

namespace CheetahApi.Extensions
{
    public static class ResultConversionExtension
    {
        public static TestResult ToResult(this TestResultDto dto)
        {
            var result = new TestResult
            {
                MarkedAnswer = dto.MarkedAnswer
            };

            if (dto.IsPassed)
            {
                result.MarkPassed();
            }
            else
            {
                result.MarkFailed();
            }

            return result;
        }
    }
}
