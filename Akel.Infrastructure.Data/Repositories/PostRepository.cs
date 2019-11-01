using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class PostRepository:IRepository<Post>
    {
        private ApplContext db;
        public PostRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Post item)
        {
            this.db.Posts.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Post item = await db.Posts.FindAsync(id);
            if (item != null)
                db.Posts.Remove(item);
        }

        public async Task<Post> Get(Guid id)
        {
            return await db.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return db.Posts;
        }

        public async  Task Update(Post item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
