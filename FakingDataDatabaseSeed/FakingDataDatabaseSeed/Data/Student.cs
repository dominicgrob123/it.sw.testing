namespace FakingDataDatabaseSeed.Data
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;

        public required List<Course> Courses { get; set; }
    }
}
