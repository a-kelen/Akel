using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
namespace Akel.Infrastructure.Data
{
    public class MessageRepository:IRepository<Message>
    {
        private ApplContext db;
        public MessageRepository(ApplContext context)
        {
            this.db = context;
        }
        public void Create(Message item)
        {
            this.db.Messages.Add(item);
        }

        public void Delete(int id)
        {
            Message item = db.Messages.Find(id);
            if (item != null)
                db.Messages.Remove(item);
        }

        public Message Get(int id)
        {
            return db.Messages.Find(id);
        }

        public IEnumerable<Message> GetAll()
        {
            return db.Messages;
        }

        public void Update(Message item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
