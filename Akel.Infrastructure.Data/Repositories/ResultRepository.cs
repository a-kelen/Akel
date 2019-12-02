using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class ResultRepository:IRepository<Result>
    {
        private ApplContext db;
        public ResultRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Result item)
        {
            this.db.Results.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Result item = await db.Results.FindAsync(id);
            if (item != null)
                db.Results.Remove(item);
        }

        public async Task<Result> Get(Guid id)
        {
            return await db.Results.FindAsync(id);
        }

        public async Task<IEnumerable<Result>> GetAll()
        {
            return db.Results.Include("Test.Audition");
        }

        public async Task Update(Result item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
