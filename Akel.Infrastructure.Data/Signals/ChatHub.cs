using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Akel.Infrastructure.Data.Signals
{
 
    public class ChatHub: Hub
    {
        UnitOfWork unit = new UnitOfWork();
        public async Task Enter(Guid userProfileId)
        {

            var subs = (await unit.Subscribers.GetAll()).Where(x => x.UserProfileId == userProfileId).Select(x => x.AuditionId);

            foreach(var a in subs)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, a.ToString());
            }

            
        }
        public async Task Send(string message, string username)
        {
            await Clients.All.SendAsync("Receive", message, username);
        }
    }
}
