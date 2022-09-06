using static Atom.Utilities.SecurityUtilities;

namespace Atom.Utilities
{
    public interface ISecurityUtilities
    {
        /// <summary>
        /// <paramref name="externalAuth"/> true if using third party to login (i.e. Google). false if using email/password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="externalAuth"></param>
        public LoginAttemptResult LoginUser(string email, string? password, bool externalAuth = false);

    }
}
