
namespace BrownfieldRefactoring
{
    public class Logger
    {
        public virtual void Log(string message)
        {
            Console.WriteLine($"Logger:         {message}");
        }
    }
}