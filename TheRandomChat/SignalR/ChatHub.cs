using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheRandomChat.Moudel;
using TheRandomChat.Services;

namespace TheRandomChat.SignalR
{
    public class ChatHub : Hub
    {


        /// <summary>
        /// This Method executes When someone Wants to Connect to Hub
        /// </summary>
        [Authorize]
        public override Task OnConnectedAsync()
        {
            //variables
            AccountControlService accountControl = new AccountControlService();
            ChatService chatService = new ChatService();
            string TargetConnectionId;
            //variables

            if (Context.User.Identity.Name == null)
                return null;

            TargetConnectionId = accountControl.IsSomeoneWaiting(Context.ConnectionId);

            if (TargetConnectionId != null)
            {
                accountControl.CreateAccount(Context.User.Identity.Name, Context.ConnectionId, TargetConnectionId, "Talking");
                chatService.CreateChat(Context.ConnectionId,TargetConnectionId);
            }
                
            else
                accountControl.CreateAccount(Context.User.Identity.Name, Context.ConnectionId);

                return base.OnConnectedAsync();
        }


        /// <summary>
        /// This Method For Send a Message to Someone
        /// </summary>
        /// <param name="Message"></param>
        [Authorize]
        public void SendMessage(string Message)
        {
            //variables
            ChatService chatService = new ChatService();
            AccountControlService accountControl = new AccountControlService();
            Connections connections = new Connections();
            Chat chat;
            //variables

            connections = accountControl.GetTheConnections(Context.User.Identity.Name);

            if (connections == null)
                return;

            chat = chatService.GetTheChat(connections);

            if (chat == null)
                return;

            chatService.SendMessage(Message, chat);

            Clients.Clients(connections.ConnectionTwo).SendAsync("ReceiveMessage",chat);
            Clients.Caller.SendAsync("ReceiveMessage",chat);

        }



    }
}
