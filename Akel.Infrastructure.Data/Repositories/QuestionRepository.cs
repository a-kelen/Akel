using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class QuestionRepository:IRepository<Question>
    {
        private ApplContext db;
        public QuestionRepository(ApplContext context)
        {
            this.db = context;
        }
        public void Create(Question item)
        {
            this.db.Questions.Add(item);
        }

        public void Delete(int id)
        {
            Question item = db.Questions.Find(id);
            if (item != null)
                db.Questions.Remove(item);
        }

        public Question Get(int id)
        {
            return db.Questions.Find(id);
        }

        public IEnumerable<Question> GetAll()
        {
            return db.Questions;
        }

        public void Update(Question item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
