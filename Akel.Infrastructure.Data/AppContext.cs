using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Akel.Domain.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Akel.Infrastructure.Data
{
    public class ApplContext :IdentityDbContext<User,IdentityRole,string>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Audition> Auditions { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Photo> Photos{ get; set; }
        public ApplContext(DbContextOptions<ApplContext> options):base(options)
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6L4JP9E\\SQLEXPRESS;Database=app;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserProfileChat>()
           .HasKey(t => new { t.UserProfileId, t.ChatId});

            modelBuilder.Entity<UserProfileChat>()
                .HasOne(sc => sc.UserProfile)
                .WithMany(s => s.Chats)
                .HasForeignKey(sc => sc.UserProfileId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfileChat>()
                .HasOne(sc => sc.Chat)
                .WithMany(c => c.Users)
                .HasForeignKey(sc => sc.ChatId)
                .OnDelete(DeleteBehavior.NoAction);
            /*modelBuilder.Entity<Message>()
                .HasOne(e=>e.UserProfile)
                .WithMany(t=>t.)
                .*/


        }
    }
}
