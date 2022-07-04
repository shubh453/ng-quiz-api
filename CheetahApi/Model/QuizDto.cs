namespace CheetahApi.Model
{
    public class QuizDto
    {
        public QuizDto(int id, string question, string answer, IEnumerable<string> options, string category)
        {
            Id = id;
            Question = question;
            Answer = answer;
            Options = options;
            Category = category;
        }
        public int Id { get; set; }
        public string Question { get; }
        public string Answer { get; }
        public string Category { get; }
        public IEnumerable<string> Options { get; }
    }
}
