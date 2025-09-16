using System.Collections.Concurrent;
using TheRandomChat.Moudel;

namespace TheRandomChat.Services
{
    public class AccountControlService
    {

        ConcurrentDictionary<string,Account> Accounts = new ConcurrentDictionary<string,Account>();

        /// <summary>
        /// Check users that already taken or not 
        /// </summary>
        /// <param name="username">the username to check</param>
        /// <returns>True is already takan , false is not</returns>
        public bool CheckUserExists(string username)
        {
            if (Accounts.TryGetValue(username, out Account account))
                return true;
            else
                return false;
        }



    }
}
