using CheetahApi.Model;

namespace CheetahApi.Extensions
{
    public static class ResultConversionExtension
    {
        public static TestResult ToResult(this TestResultDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var result = new TestResult
            {
                MarkedAnswers = dto.MarkedAnswer
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

        public static TestResultDto ToDto(this TestResult result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            return new TestResultDto(result.MarkedAnswers, result.Score, result.ResultStatus == Result.Passed);
        }
    }
}
