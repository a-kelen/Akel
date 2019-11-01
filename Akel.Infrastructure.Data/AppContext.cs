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
         //   Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public ApplContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-T2OE9DJB\\SQLEXPRESS;Database=app;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Answer>().HasKey(p => p.Id);
            mb.Entity<Answer>()
                .HasOne(p => p.Question)
                .WithMany(p => p.Answers)
                .HasForeignKey(p => p.QuestionId);
            //
            mb.Entity<Audition>().HasKey(p => p.Id);
            mb.Entity<Audition>().Property(p => p.Name).IsRequired();
            mb.Entity<Audition>()
                .HasOne(p => p.Chat)
                .WithOne(p => p.Audition)
                .HasForeignKey<Chat>(p => p.AuditionId);
                
            mb.Entity<Audition>()
                .HasOne(p => p.UserProfile)
                .WithMany(p => p.Auditions)
                .HasForeignKey(p=>p.UserProfileId);
            //
            mb.Entity<Chat>().HasKey(p => p.Id);
            //
            mb.Entity<Comment>().HasKey(p => p.Id);
            mb.Entity<Comment>()
                .HasOne(p => p.UserProfile)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.NoAction);
            mb.Entity<Comment>()
                .HasOne(p => p.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.PostId);
            //
            mb.Entity<Friend>().HasKey(p => p.Id);
            mb.Entity<Friend>()
                .HasOne(p => p.UserProfile)
                .WithMany(p => p.Friends)
                .HasForeignKey(p=>p.UserProfileId);
            
            //
            mb.Entity<Message>().HasKey(p => p.Id);
            mb.Entity<Message>()
                .HasOne(p => p.UserProfile)
                .WithMany(p => p.Messages)
                .HasForeignKey(p => p.UserProfileId);
            mb.Entity<Message>()
                .HasOne(p => p.Chat)
                .WithMany(p => p.Messages)
                .HasForeignKey(p => p.ChatId);
            //
            mb.Entity<Photo>().HasKey(p => p.Id);
            mb.Entity<Photo>()
                .HasOne(p => p.Post)
                .WithOne(p => p.Photo)
                .HasForeignKey<Post>(p=>p.PhotoId);
            //
            mb.Entity<Post>().HasKey(p => p.Id);
            mb.Entity<Post>()
                .HasOne(p => p.Audition)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.AuditionId);
            
            //
            mb.Entity<Question>().HasKey(p => p.Id);
            mb.Entity<Question>()
                .HasOne(p => p.Test)
                .WithMany(p => p.Questions)
                .HasForeignKey(p => p.TestId);
            //
            mb.Entity<Result>().HasKey(p => p.Id);
            mb.Entity<Result>()
               .HasOne(p => p.Test)
               .WithMany(p => p.Results)
               .HasForeignKey(p => p.TestId);
            mb.Entity<Result>()
               .HasOne(p => p.UserProfile)
               .WithMany(p => p.Results)
               .HasForeignKey(p => p.UserProfileId);
            //
            mb.Entity<Subscriber>().HasKey(p => p.Id);
            mb.Entity<Subscriber>()
               .HasOne(p => p.Audition)
               .WithMany(p => p.Subscribers)
               .HasForeignKey(p => p.AuditionId);
            mb.Entity<Subscriber>()
               .HasOne(p => p.UserProfile)
               .WithMany(p => p.Subscribers)
               .HasForeignKey(p => p.UserProfileId);
            //
            mb.Entity<Test>().HasKey(p => p.Id);
            mb.Entity<Test>()
               .HasOne(p => p.Audition)
               .WithMany(p => p.Tests)
               .HasForeignKey(p => p.AuditionId);
            //
            mb.Entity<User>()
                .HasOne(p => p.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);
            //
            mb.Entity<UserProfile>().HasKey(p => p.Id);
            //
            mb.Entity<UserProfileChat>()
           .HasKey(t => new { t.UserProfileId, t.ChatId });
            mb.Entity<UserProfileChat>()
                .HasOne(sc => sc.UserProfile)
                .WithMany(s => s.Chats)
                .HasForeignKey(sc => sc.UserProfileId)
                .OnDelete(DeleteBehavior.NoAction);
            mb.Entity<UserProfileChat>()
                .HasOne(sc => sc.Chat)
                .WithMany(c => c.Users)
                .HasForeignKey(sc => sc.ChatId)
                .OnDelete(DeleteBehavior.NoAction);
         


        }
    }
}
