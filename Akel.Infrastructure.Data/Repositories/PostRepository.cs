using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
namespace Akel.Infrastructure.Data
{
    public class PostRepository:IRepository<Post>
    {
        private ApplContext db;
        public PostRepository(ApplContext context)
        {
            this.db = context;
        }
        public void Create(Post item)
        {
            this.db.Posts.Add(item);
        }

        public void Delete(int id)
        {
            Post item = db.Posts.Find(id);
            if (item != null)
                db.Posts.Remove(item);
        }

        public Post Get(int id)
        {
            return db.Posts.Find(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts;
        }

        public void Update(Post item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
