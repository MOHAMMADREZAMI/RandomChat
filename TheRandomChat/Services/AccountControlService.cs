using System.Collections.Concurrent;
using System.Security.Principal;
using TheRandomChat.Moudel;

namespace TheRandomChat.Services
{
    public class AccountControlService
    {
        //variables
        private static ConcurrentDictionary<string,Account> Accounts = new ConcurrentDictionary<string,Account>();
        //variables


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

        /// <summary>
        /// Create User's Account with information
        /// </summary>
        /// <param name="username">username is one of the information of user</param>
        /// <param name="ConnectionId">ConnectionId is one of the information of user</param>
        /// <param name="TargetConnectionId"> TargetConnectionId is one of the information of user</param>
        /// <param name="status"> status is one of the information of user</param>
        public void CreateAccount(string username,string ConnectionId,string TargetConnectionId = null ,string status = "Waiting")
        {
            //variables
            Account account;
            //variables

            account = new Account()
            {
                username = username,
                ConnectionId = ConnectionId,
                status = status,
                TargetConnectionId = TargetConnectionId
                
            };

            Accounts.TryAdd(username,account);
        }


        /// <summary>
        /// Check the Users , who is waiting
        /// </summary>
        /// <returns>If someone is waiting , method returrns a ConnectionId of it , but nobody is waiting method returns null</returns>
        public string IsSomeoneWaiting(string ConnectionId)
        {
            //variables
            Account account;
            //variables

            for (int Check = 0; Check < Accounts.Count; Check++)
            {

                account = Accounts.ElementAt(Check).Value;

                if(account.status == "Waiting")
                {
                    account.status = "Talking";
                    account.TargetConnectionId = ConnectionId;
                    return account.ConnectionId;
                }
            }

            return null;
        }

        /// <summary>
        /// Get The Two Connections (ConnectionId,TargetConnectionId)
        /// </summary>
        /// <param name="username"></param>
        /// <returns>return a 'Connections' it has got two Connections</returns>
        public Connections GetTheConnections(string username)
        {
            //variables
            Connections connections;
            //variables

            Accounts.TryGetValue(username, out var account);

                if (account.TargetConnectionId != null)
                {
                    return connections = new Connections()
                    {
                        ConnectionOne = account.ConnectionId,
                        ConnectionTwo = account.TargetConnectionId
                    };
                }
                else
                    return null;
        }

        /// <summary>
        /// Remove a Account
        /// </summary>
        /// <param name="username"></param>
        public void DeleteAccount(string username)
        {
            Accounts.TryRemove(username, out var account);
        }

        /// <summary>
        /// get the username of TargetConnectionId
        /// </summary>
        /// <param name="TargetConnectionId"></param>
        /// <returns>return a username (string)</returns>
        public string GetTheUsername(string TargetConnectionId)
        {

            //variables
            Account account;
            //variables

            for (int Check = 0; Check < Accounts.Count; Check++)
            {
                account = Accounts.ElementAt(Check).Value;

                if (account.ConnectionId == TargetConnectionId)
                    return account.username;

            }

            return null;

        }

        /// <summary>
        /// Change status of user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="status"></param>
        public void ChangeStatus(string username, string status)
        {

            if (Accounts.TryGetValue(username, out var account))
            {
                account.status = status;
            }
            else
                return;

        }

        public int GetCount()
        {
            return Accounts.Count;
        }
    }
}
