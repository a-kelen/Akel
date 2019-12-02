using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Interface;
using Akel.Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data.Repositories
{
    public class PhotoRepository : IRepository<Photo>
    {
        private ApplContext db;
        public PhotoRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(Photo item)
        {
            this.db.Photos.Add(item);
        }

        public async Task Delete(Guid id)
        {
            Photo item = db.Photos.Find(id);
            if (item != null)
                db.Photos.Remove(item);
        }

        public async Task<Photo> Get(Guid id)
        {
            return await db.Photos.FindAsync(id);
        }

        public async Task<IEnumerable<Photo>> GetAll()
        {
            return await db.Photos.ToListAsync();
        }

        public async Task Update(Photo item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
