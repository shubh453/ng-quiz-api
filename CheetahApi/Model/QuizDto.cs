namespace CheetahApi.Model
{
    public class QuizDto
    {
        public QuizDto() { }
        public QuizDto(int id, string question, string answer, IEnumerable<string> options, string category, string? additionalText)
        {
            Id = id;
            Question = question;
            Answer = answer;
            Options = options;
            Category = category;
            AdditionalText = additionalText;
        }
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; }
        public string? AdditionalText { get; set; }
        public IEnumerable<string> Options { get; set; }
    }
}
