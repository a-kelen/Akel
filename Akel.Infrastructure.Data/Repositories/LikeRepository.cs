using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class LikeRepository : IRepository<Like>
    {
        private ApplContext db;
        public LikeRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Like item)
        {
            this.db.Likes.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Like item = db.Likes.Find(id);
            if (item != null)
                db.Likes.Remove(item);
        }

        public async Task<Like> Get(Guid id)
        {
            return await db.Likes.FindAsync(id);
        }

        public async Task<IEnumerable<Like>> GetAll()
        {
            return await db.Likes.ToListAsync();
        }

        public async Task Update(Like item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
