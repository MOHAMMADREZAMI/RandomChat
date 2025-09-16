using System.Collections.Concurrent;
using TheRandomChat.Moudel;

namespace TheRandomChat.Services
{
    public class ChatService
    {

        private static ConcurrentDictionary<string, Chat> Chats = new ConcurrentDictionary<string, Chat>();

        public void CreateChat(string Connectionid,string TargetConnectionId)
        {
            



        }




    }
}
