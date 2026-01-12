using Microsoft.EntityFrameworkCore;

namespace FakingDataDatabaseSeed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Course subscriptions");

            using (var cm = new CourseManagerContext())
            {
                foreach (var student in cm.Students
                                          .Include(s => s.Courses))
                {
                    Console.WriteLine($"{student.Name}, {student.Firstname}");
                    foreach (var course in student.Courses)
                    {
                        Console.WriteLine($"  - {course.Name}");
                    }
                }
            }
        }
    }
}
