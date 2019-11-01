using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akel.Domain.Core;
using Akel.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class UserProfileRepository:IRepository<UserProfile>
    {
        private ApplContext db;
        public UserProfileRepository(ApplContext context)
        {
            this.db = context;
        }
        public async Task Create(UserProfile item)
        {
            this.db.UserProfiles.Add(item);
        }

        public async Task Delete(Guid id)
        {
            UserProfile item = await db.UserProfiles.FindAsync(id);
            if (item != null)
                db.UserProfiles.Remove(item);
        }

        public async Task<UserProfile> Get(Guid id)
        {
            return await db.UserProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            return db.UserProfiles;
            ;
        }

        public async Task Update(UserProfile item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
