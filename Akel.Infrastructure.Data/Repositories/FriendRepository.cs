using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class FriendRepository:IRepository<Friend>
    {
        private ApplContext db;
        public FriendRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Friend item)
        {
            this.db.Friends.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Friend item = await db.Friends.FindAsync(id);
            if (item != null)
                db.Friends.Remove(item);
        }

        public async Task<Friend> Get(Guid id)
        {
            return await db.Friends.FindAsync(id);
        }

        public async Task<IEnumerable<Friend>> GetAll()
        {
            return db.Friends;
        }

        public async Task Update(Friend item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
