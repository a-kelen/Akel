using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class TestRepository:IRepository<Test>
    {
        private AppContext db;
        public TestRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(Test item)
        {
            this.db.Tests.Add(item);
        }

        public void Delete(int id)
        {
            Test item = db.Tests.Find(id);
            if (item != null)
                db.Tests.Remove(item);
        }

        public Test Get(int id)
        {
            return db.Tests.Find(id);
        }

        public IEnumerable<Test> GetAll()
        {
            return db.Tests;
        }

        public void Update(Test item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
