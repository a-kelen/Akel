using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class ResultRepository
    {
        private AppContext db;
        public ResultRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(Result item)
        {
            this.db.Results.Add(item);
        }

        public void Delete(int id)
        {
            Result item = db.Results.Find(id);
            if (item != null)
                db.Results.Remove(item);
        }

        public Result Get(int id)
        {
            return db.Results.Find(id);
        }

        public IEnumerable<Result> GetAll()
        {
            return db.Results;
        }

        public void Update(Result item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
