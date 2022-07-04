namespace CheetahApi.Model
{
    public class Quiz
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public IEnumerable<string> Options { get; set; } = Enumerable.Empty<string>();

        public string Answer { get; set; }

        public string Category { get; set; }

        public Guid UniqueIdentifier { get; set; } = Guid.NewGuid();

        public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;

        public DateTime UpdatedOnUtc { get; set; }
    }
}
