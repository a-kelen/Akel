using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Core;
using Akel.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Akel.Infrastructure.Data
{
    public class UserProfileRepository:IRepository<UserProfile>
    {
        private AppContext db;
        public UserProfileRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(UserProfile item)
        {
            this.db.UserProfiles.Add(item);
        }

        public void Delete(int id)
        {
            UserProfile item = db.UserProfiles.Find(id);
            if (item != null)
                db.UserProfiles.Remove(item);
        }

        public UserProfile Get(int id)
        {
            return db.UserProfiles.Find(id);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return db.UserProfiles;
            ;
        }

        public void Update(UserProfile item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
