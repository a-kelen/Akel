using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class AnswerRepository :IRepository<Answer>
    {
        private ApplContext db;
        public AnswerRepository(ApplContext context)
        {
            this.db = context;
        }
        public void Create(Answer item)
        {
            this.db.Answers.Add(item);
        }

        public void Delete(int id)
        {
            Answer item = db.Answers.Find(id);
            if (item != null)
                db.Answers.Remove(item);
        }

        public Answer Get(int id)
        {
            return db.Answers.Find(id);
        }

        public IEnumerable<Answer> GetAll()
        {
            return db.Answers;
        }

        public void Update(Answer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
