using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
namespace Akel.Infrastructure.Data
{
    public class FriendRepository:IRepository<Friend>
    {
        private ApplContext db;
        public FriendRepository(ApplContext context)
        {
            this.db = context;
        }
        public void Create(Friend item)
        {
            this.db.Friends.Add(item);
        }

        public void Delete(int id)
        {
            Friend item = db.Friends.Find(id);
            if (item != null)
                db.Friends.Remove(item);
        }

        public Friend Get(int id)
        {
            return db.Friends.Find(id);
        }

        public IEnumerable<Friend> GetAll()
        {
            return db.Friends;
        }

        public void Update(Friend item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
