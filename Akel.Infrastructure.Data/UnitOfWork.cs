using Akel.Domain.Core;
using Akel.Domain.Interface;
using Akel.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class UnitOfWork: iUnitOfWork
    {
        public UnitOfWork()
        {
            this.db = new ApplContext();
        }
        ApplContext db;
        IRepository<Answer> AnswerRepository;
        IRepository<Audition> AuditionRepository;
        IRepository<Chat> ChatRepository;
        IRepository<Comment> CommentRepository;
        IRepository<Friend> FriendRepository;
        IRepository<Message> MessageRepository;
        IRepository<Post> PostRepository;
        IRepository<Question> QuestionRepository;
        IRepository<Result> ResultRepository;
        IRepository<Test> TestRepository;
        IRepository<UserProfile> UserProfileRepository;
        IRepository<Subscriber> SubscriberRepository;
        IRepository<Photo> PhotoRepository;
        IRepository<Like> LikeRepository;

        public IRepository<Answer> Answers { 
            get {
                return AnswerRepository ?? (AnswerRepository = new AnswerRepository(db)); 
            }
            set
            {
                AnswerRepository = value;
            }
        }
        public IRepository<Audition> Auditions
        {
            get
            {
                return AuditionRepository ?? (AuditionRepository = new AuditionRepository(db));
            }
            set
            {
                AuditionRepository = value;
            }
        }
        public IRepository<Chat> Chats
        {
            get
            {
                return ChatRepository ?? (ChatRepository = new ChatRepository(db));
            }
            set
            {
                ChatRepository = value;
            }
        }
        public IRepository<Comment> Comments
        {
            get
            {
                return CommentRepository ?? (CommentRepository = new CommentRepository(db));
            }
            set
            {
                CommentRepository = value;
            }
        }
        public IRepository<Friend> Friends
        {
            get
            {
                return FriendRepository ?? (FriendRepository = new FriendRepository(db));
            }
            set
            {
               FriendRepository = value;
            }
        }
        public IRepository<Message> Messages
        {
            get
            {
                return MessageRepository ?? (MessageRepository = new MessageRepository(db));
            }
            set
            {
                MessageRepository = value;
            }
        }
        public IRepository<Post> Posts
        {
            get
            {
                return PostRepository ?? (PostRepository = new PostRepository(db));
            }
            set
            {
                PostRepository = value;
            }
        }
        public IRepository<Question> Questions
        {
            get
            {
                return QuestionRepository ?? (QuestionRepository = new QuestionRepository(db));
            }
            set
            {
                QuestionRepository = value;
            }
        }
        public IRepository<Result> Results 
        {
            get
            {
                return ResultRepository ?? (ResultRepository = new ResultRepository(db));
            }
            set
            {
                ResultRepository = value;
            }
        }
        public IRepository<Test> Tests
        {
            get
            {
                return TestRepository ?? (TestRepository = new TestRepository(db));
            }
            set
            {
                TestRepository = value;
            }
        }
        public IRepository<UserProfile> UserProfiles
        {
            get
            {
                return UserProfileRepository ?? (UserProfileRepository = new UserProfileRepository(db));
            }
            set
            {
                UserProfileRepository = value;
            }
        }
        public IRepository<Subscriber> Subscribers
        {
            get
            {
                return SubscriberRepository ?? (SubscriberRepository = new SubscriberRepository(db));
            }
            set
            {
                SubscriberRepository = value;
            }
        }
        public IRepository<Photo> Photos
        {
            get
            {
                return PhotoRepository ?? (PhotoRepository = new PhotoRepository(db));
            }
            set
            {
                PhotoRepository = value;
            }
        }
        public IRepository<Like> Likes
        {
            get
            {
                return LikeRepository ?? (LikeRepository = new LikeRepository(db));
            }
            set
            {
                LikeRepository = value;
            }
        }


        public async Task Save()
        {
             db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
