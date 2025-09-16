using System.Collections.Concurrent;
using System.Runtime;
using TheRandomChat.Moudel;

namespace TheRandomChat.Services
{
    public class ChatService
    {
        //variables
        private static ConcurrentDictionary<string, Chat> Chats = new ConcurrentDictionary<string, Chat>();
        //variables


        /// <summary>
        /// This Method execute When The Users Dont have any Chat History
        /// </summary>
        /// <param name="Connectionid"></param>
        /// <param name="TargetConnectionId"></param>
        public void CreateChat(string Connectionid,string TargetConnectionId)
        {
            Chats.TryAdd(Connectionid + ":" + TargetConnectionId, new Chat());
        }


        /// <summary>
        /// This Method execute When the users want to have their Chat History
        /// </summary>
        /// <param name="connections"></param>
        /// <returns></returns>
        public Chat GetTheChat(Connections connections)
        {

            for(int i = 0; i <= 1; i++)
            {

                Chats.TryGetValue(connections.ConnectionOne + ":" + connections.ConnectionTwo, out var chat);

                if (chat != null)
                    return chat;

            }

            return null;
        }

        /// <summary>
        /// Send a Message
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Chat"></param>
        public void SendMessage (string Message,Chat Chat)
        {
            Chat.chat.Add(Message);
        }


    }
}
