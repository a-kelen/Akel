using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class TestRepository:IRepository<Test>
    {
        private ApplContext db;
        public TestRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Test item)
        {
            this.db.Tests.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Test item =await db.Tests.FindAsync(id);
            if (item != null)
                db.Tests.Remove(item);
        }

        public async Task<Test> Get(Guid id)
        {
            return await db.Tests.FindAsync(id);
        }

        public async Task<IEnumerable<Test>> GetAll()
        {
            var res = db.Tests.Include("Questions.Answers").Include("Audition");
            return res;
        }

        public async Task Update(Test item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
