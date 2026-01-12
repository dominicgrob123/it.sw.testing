namespace BrownfieldRefactoring
{
    public class User
    {
        public required string UserName { get; set; }

        public required string EMail { get; set; }

        public bool IsConsistent()
        {
            /* Just making up some logic here to justify a method
             * without having to build a huge, distracting case. :-) */
            return EMail.StartsWith(UserName);
        }
    }
}
