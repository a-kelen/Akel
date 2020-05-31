using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akel.Domain.Core;
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
        public class MessageVM
        {
            public string message { get; set; }
            public Guid userId { get; set; }
            public Guid auditionId { get; set; }
            public string username { get; set; }
        }
        public async Task Send(MessageVM m)
        {
            Chat chat = (await unit.Chats.GetAll()).FirstOrDefault(x => x.AuditionId == m.auditionId);
            Message message;
            if (chat != null)
            {
                message = new Message { ChatId = chat.Id, Text = m.message, UserProfileId = m.userId, Date = DateTime.Now };
                await unit.Messages.Create(message);
                await unit.Save();
                 await Clients.Group(m.auditionId.ToString()).SendAsync("Receive",message.ChatId, m.userId , m.username, message.Text , message.Date );
            }  
        }
    }
}
