using Akel.Domain.Core;
using Akel.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Infrastructure.Data
{
    public class MemberRepository:IRepository<Member>
    {
        private AppContext db;
        public MemberRepository(AppContext context)
        {
            this.db = context;
        }
        public void Create(Member item)
        {
            this.db.Members.Add(item);
        }

        public void Delete(int id)
        {
            Member item = db.Members.Find(id);
            if (item != null)
                db.Members.Remove(item);
        }

        public Member Get(int id)
        {
            return db.Members.Find(id);
        }

        public IEnumerable<Member> GetAll()
        {
            return db.Members;
        }

        public void Update(Member item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
