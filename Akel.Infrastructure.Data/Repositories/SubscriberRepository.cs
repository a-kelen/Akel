using Akel.Domain.Core;
using Akel.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Infrastructure.Data.Repositories
{
    class SubscriberRepository:IRepository<Subscriber>
    {
        private AppContext db;
        public SubscriberRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(Subscriber item)
        {
            this.db.Subscribers.Add(item);
        }

        public void Delete(int id)
        {
            Subscriber item = db.Subscribers.Find(id);
            if (item != null)
                db.Subscribers.Remove(item);
        }

        public Subscriber Get(int id)
        {
            return db.Subscribers.Find(id);
        }

        public IEnumerable<Subscriber> GetAll()
        {
            return db.Subscribers;
        }

        public void Update(Subscriber item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
