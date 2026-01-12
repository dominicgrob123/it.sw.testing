using Bogus;

namespace FakingData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string DELIMITER = ", ";
            string[] studies = [ "Metzger"
                               , "Gärtner"
                               , "Softwareentwickler"
                               , "Clown"
                               , "Industrieschauspieler"];

            var faker = new Faker<Student>("de_CH")
              .RuleFor(s => s.Name, f => f.Person.LastName)
              .RuleFor(s => s.FirstName, f => f.Person.FirstName)
              .RuleFor(s => s.Profession, f => f.PickRandom(studies));

            var students = faker.Generate(6);

            var maxNameLength = students.Max(s => s.Name.Length
                              + s.FirstName.Length
                              + DELIMITER.Length);

            foreach (var student in students)
            {
                var fullName = $"{student.Name}{DELIMITER}{student.FirstName}";
                var spacer = new string(' ', maxNameLength - fullName.Length);

                Console.WriteLine($"{fullName} {spacer} {student.Profession}");
            }
        }
    }
}
