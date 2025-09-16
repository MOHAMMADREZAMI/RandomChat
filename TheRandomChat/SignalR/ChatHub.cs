using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheRandomChat.Moudel;
using TheRandomChat.Services;

namespace TheRandomChat.SignalR
{
    [Authorize]
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
                return Task.CompletedTask;

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
        public async void SendMessage(string Message)
        {
            //variables
            ChatService chatService = new ChatService();
            AccountControlService accountControl = new AccountControlService();
            Connections connections = new Connections();
            Chat chat;
            //variables

            connections = accountControl.GetTheConnections(Context.User.Identity.Name);

            if (connections == null)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Please wait to connect to someone");
                return;
            }

            chat = chatService.GetTheChat(connections);

            if (chat == null)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Please wait to connect to someone");
                return;
            }

            chatService.SendMessage(Context.User.Identity.Name + " : " + Message, chat);

            await Clients.Clients(connections.ConnectionTwo).SendAsync("ReceiveMessage",chat.chat);
            await Clients.Caller.SendAsync("ReceiveMessage", chat.chat);

        }

        /// <summary>
        /// This Method executes When someone Wants to Disconnect to Hub
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        public override Task OnDisconnectedAsync(Exception? exception)
        {

            //variables
            AccountControlService accountControl = new AccountControlService();
            Connections connections;
            ChatService chatService = new ChatService();
            string username;
            int count;
            //variables

            count = accountControl.GetCount();

            connections = accountControl.GetTheConnections(Context.User.Identity.Name);
            if (connections != null && count != 1)
            {
                username = accountControl.GetTheUsername(connections.ConnectionTwo);
                accountControl.ChangeStatus(username, "Waiting");
                chatService.DeleteChat(connections);

            }

            accountControl.DeleteAccount(Context.User.Identity.Name);

            
           
            return base.OnDisconnectedAsync(exception);
        }


    }
}
