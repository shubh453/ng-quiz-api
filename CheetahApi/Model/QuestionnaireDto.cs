namespace CheetahApi.Model
{
    public class QuestionnaireDto
    {
        public QuestionnaireDto(int id, string category, IEnumerable<QuizDto> quizzes)
        {
            Id = id;
            Category = category;
            Quizzes = quizzes;
        }

        public int Id { get; set; }

        public string Category { get; set; }

        public IEnumerable<QuizDto> Quizzes { get; init; }
    }
}
