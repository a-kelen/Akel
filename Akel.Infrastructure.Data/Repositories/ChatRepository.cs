using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akel.Domain.Core;
using Akel.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class ChatRepository : IRepository<Chat>
    {
        private ApplContext db;
        public ChatRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Chat item)
        {
           this.db.Chats.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Chat chat = await db.Chats.FindAsync(id);
            if (chat != null)
                db.Chats.Remove(chat);
        }

        public async Task<Chat> Get(Guid id)
        {
            return await db.Chats.FindAsync(id);
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            return  db.Chats;
        }

        public async Task Update(Chat item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
