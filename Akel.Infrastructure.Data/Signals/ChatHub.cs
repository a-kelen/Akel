using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Akel.Infrastructure.Data.Signals
{
 
    public class ChatHub: Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
            
        }
    }
}
