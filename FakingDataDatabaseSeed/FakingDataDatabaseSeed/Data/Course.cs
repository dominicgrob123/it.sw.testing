namespace FakingDataDatabaseSeed.Data
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public required List<Student> Students { get; set; }
    }
}
