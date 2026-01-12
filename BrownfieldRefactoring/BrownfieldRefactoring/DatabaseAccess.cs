namespace BrownfieldRefactoring
{
    public class DatabaseAccess
    {
        public DatabaseAccess()
        {
        }

        public virtual bool SaveUser(User user)
        {
            Console.WriteLine($"DatabaseAccess: Saving user {user.UserName}, {user.EMail}");
            return true;
        }
    }
}