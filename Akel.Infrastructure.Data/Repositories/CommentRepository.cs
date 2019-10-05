using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
namespace Akel.Infrastructure.Data
{
    public class CommentRepository:IRepository<Comment>
    {
        private AppContext db;
        public CommentRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(Comment item)
        {
            this.db.Comments.Add(item);
        }

        public void Delete(int id)
        {
            Comment item = db.Comments.Find(id);
            if (item != null)
                db.Comments.Remove(item);
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
