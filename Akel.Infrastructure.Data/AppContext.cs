using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Core;
namespace Akel.Infrastructure.Data
{
    public class AppContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Audition> Auditions { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Member> Members { get; set; }
        public  DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
