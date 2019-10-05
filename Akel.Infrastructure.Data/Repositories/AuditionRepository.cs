using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
namespace Akel.Infrastructure.Data
{
    public class AuditionRepository:IRepository<Audition>
    {
        private AppContext db;
        public AuditionRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(Audition item)
        {
            this.db.Auditions.Add(item);
        }

        public void Delete(int id)
        {
            Audition item = db.Auditions.Find(id);
            if (item != null)
                db.Auditions.Remove(item);
        }

        public Audition Get(int id)
        {
            return db.Auditions.Find(id);
        }

        public IEnumerable<Audition> GetAll()
        {
            return db.Auditions;
        }

        public void Update(Audition item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
