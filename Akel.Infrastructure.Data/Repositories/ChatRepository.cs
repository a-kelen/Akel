using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Core;
using Akel.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class ChatRepository : IRepository<Chat>
    {
        private AppContext db;
        public ChatRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(Chat item)
        {
           this.db.Chats.Add(item);
        }

        public void Delete(int id)
        {
            Chat chat = db.Chats.Find(id);
            if (chat != null)
                db.Chats.Remove(chat);
        }

        public Chat Get(int id)
        {
            return db.Chats.Find(id);
        }

        public IEnumerable<Chat> GetAll()
        {
            return db.Chats;
        }

        public void Update(Chat item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
