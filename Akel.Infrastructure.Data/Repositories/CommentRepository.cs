using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class CommentRepository:IRepository<Comment>
    {
        private ApplContext db;
        public CommentRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Comment item)
        {
            this.db.Comments.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Comment item = await db.Comments.FindAsync(id);
            if (item != null)
                db.Comments.Remove(item);
        }

        public async Task<Comment> Get(Guid id)
        {
            return await db.Comments.Include("UserProfile").FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return db.Comments.Include(x => x.UserProfile );
        }

        public async Task Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
