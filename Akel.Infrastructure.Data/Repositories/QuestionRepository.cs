using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class QuestionRepository:IRepository<Question>
    {
        private ApplContext db;
        public QuestionRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Question item)
        {
            this.db.Questions.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Question item = await db.Questions.FindAsync(id);
            if (item != null)
                db.Questions.Remove(item);
        }

        public async Task<Question> Get(Guid id)
        {
            return await db.Questions.FindAsync(id);
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return  db.Questions;
        }

        public async Task Update(Question item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
