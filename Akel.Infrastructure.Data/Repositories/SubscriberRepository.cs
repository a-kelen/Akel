using Akel.Domain.Core;
using Akel.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class SubscriberRepository:IRepository<Subscriber>
    {
        private ApplContext db;
        public SubscriberRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Subscriber item)
        {
            this.db.Subscribers.Add(item);
            this.db.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            Subscriber item = await db.Subscribers.FindAsync(id);
            if (item != null)
                db.Subscribers.Remove(item);
        }

        public async Task<Subscriber> Get(Guid id)
        {
            return await db.Subscribers.FindAsync(id);
        }

        public async Task<IEnumerable<Subscriber>> GetAll()
        {
            return db.Subscribers.Include("Audition.Chat.Messages.UserProfile");
        }

        public async Task Update(Subscriber item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
