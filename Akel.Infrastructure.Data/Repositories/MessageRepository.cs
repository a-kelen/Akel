using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class MessageRepository:IRepository<Message>
    {
        private ApplContext db;
        public MessageRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Message item)
        {
            this.db.Messages.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Message item = await db.Messages.FindAsync(id);
            if (item != null)
                db.Messages.Remove(item);
        }

        public async Task<Message> Get(Guid id)
        {
            return await db.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return db.Messages;
        }

        public async Task Update(Message item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
