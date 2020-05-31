using Akel.Domain.Core;
using Akel.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public interface iUnitOfWork : IDisposable
    {
        IRepository<Answer> Answers { get; set; }

        IRepository<Audition> Auditions { get; set; }

        IRepository<Chat> Chats { get; set; }

        IRepository<Comment> Comments { get; set; }

        IRepository<Friend> Friends { get; set; }

        IRepository<Message> Messages { get; set; }

        IRepository<Post> Posts { get; set; }

        IRepository<Question> Questions { get; set; }

        IRepository<Result> Results { get; set; }

        IRepository<Test> Tests { get; set; }

        IRepository<UserProfile> UserProfiles { get; set; }

        IRepository<Subscriber> Subscribers { get; set; }

        IRepository<Like> Likes { get; set; }

        Task Save();
        
    }
}
