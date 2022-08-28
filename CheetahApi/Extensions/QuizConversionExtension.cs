using CheetahApi.Model;

namespace CheetahApi.Extensions
{
    public static class QuizConversionExtension
    {
        public static QuizDto ToDto(this Quiz quiz)
        {
            if (quiz == null) throw new ArgumentNullException(nameof(quiz));

            return new QuizDto(quiz.Id, quiz.Question, quiz.Answer, quiz.Options, quiz.Category, quiz.AdditionalText);
        }

        public static Quiz ToModel(this QuizDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Quiz
            {
                Answer = dto.Answer,
                Options = dto.Options,
                Question = dto.Question,
                Category = dto.Category,
                AdditionalText = dto.AdditionalText,
                UniqueIdentifier = Guid.NewGuid(),
                UpdatedOnUtc = DateTime.UtcNow,
                CreatedOnUtc = DateTime.UtcNow
            };
        }
    }
}
