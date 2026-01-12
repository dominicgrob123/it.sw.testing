namespace BrownfieldRefactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager userManager = new();

            userManager.AddUser("Max Mustermann", "max@example.com");
        }
    }
}
