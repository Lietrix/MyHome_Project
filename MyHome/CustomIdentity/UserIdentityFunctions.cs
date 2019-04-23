namespace MyHome.CustomIdentity
{
    public class UserIdentityFunctions
    {
        public static string GetUsername(string identity) => identity.Substring(0, identity.IndexOf('@'));
    }
}