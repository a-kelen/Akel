using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class AnswerRepository :IRepository<Answer>
    {
        private ApplContext db;
        public AnswerRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Answer item)
        {
             this.db.Answers.Add(item);
        }

        public async Task Delete(Guid id)
        {
             Answer item = db.Answers.Find(id);
            if (item != null)
                db.Answers.Remove(item);
        }

        public async Task<Answer> Get(Guid id)
        {
            return await db.Answers.FindAsync(id);
        }

        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await db.Answers.ToListAsync();
        }

        public async Task Update(Answer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
