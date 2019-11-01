using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class AuditionRepository:IRepository<Audition>
    {
        private ApplContext db;
        public AuditionRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Audition item)
        {
            this.db.Auditions.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Audition item = await  db.Auditions.FindAsync(id);
            if (item != null)
                db.Auditions.Remove(item);
        }

        public async Task<Audition> Get(Guid id)
        {
            return await db.Auditions.FindAsync(id);
        }

        public async Task<IEnumerable<Audition>> GetAll()
        {
            return db.Auditions;
        }

        public async Task Update(Audition item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
