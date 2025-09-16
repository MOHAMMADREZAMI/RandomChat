using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheRandomChat.Services;

namespace TheRandomChat.SignalR
{
    public class ChatHub : Hub
    {
        [Authorize]
        public override Task OnConnectedAsync()
        {
            //variables
            AccountControlService accountControl = new AccountControlService();
            string TargetConnectionId;
            //variables

            if (Context.User.Identity.Name == null)
                return null;

            TargetConnectionId = accountControl.IsSomeoneWaiting(Context.ConnectionId);

            if (TargetConnectionId != null)
            {
                accountControl.CreateAccount(Context.User.Identity.Name, Context.ConnectionId, TargetConnectionId, "Talking");

            }
                
            else
                accountControl.CreateAccount(Context.User.Identity.Name, Context.ConnectionId);

                return base.OnConnectedAsync();
        }



    }
}
