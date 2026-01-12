namespace BrownfieldRefactoring
{
    class UserManager
    {
        private readonly Logger logger = new();
        private readonly DatabaseAccess dbAccess = new();

        public void AddUser(string name, string email)
        {
            // Logik zum Hinzufügen eines Benutzers
            logger.Log($"Adding user {name}");

            var user = new User()
            {
                UserName = name,
                EMail = email
            };

            if (user.IsConsistent())
            {
                if (dbAccess.SaveUser(user))
                {
                    logger.Log("SaveUser succeeded!");
                }
                else
                {
                    logger.Log("SaveUser failed!");
                }
            }
            else
            {
                logger.Log("User data is incosistent, no user saved.");
            }
        }
    }
}
