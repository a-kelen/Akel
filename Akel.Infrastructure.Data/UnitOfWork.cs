using Akel.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Data
{
    public class UnitOfWork:IDisposable
    {
        public UnitOfWork()
        {
            this.db = new ApplContext();
        }
        ApplContext db;
        AnswerRepository AnswerRepository;
        AuditionRepository AuditionRepository;
        ChatRepository ChatRepository;
        CommentRepository CommentRepository;
        FriendRepository FriendRepository;
        MessageRepository MessageRepository;
        PostRepository PostRepository;
        QuestionRepository QuestionRepository;
        ResultRepository ResultRepository;
        TestRepository TestRepository;
        UserProfileRepository UserProfileRepository;
        SubscriberRepository SubscriberRepository;
        PhotoRepository PhotoRepository;
        LikeRepository LikeRepository;

        public AnswerRepository Answers { 
            get {
                return AnswerRepository ?? (AnswerRepository = new AnswerRepository(db)); 
            } 
        }
        public AuditionRepository Auditions
        {
            get
            {
                return AuditionRepository ?? (AuditionRepository = new AuditionRepository(db));
            }
        }
        public ChatRepository Chats
        {
            get
            {
                return ChatRepository ?? (ChatRepository = new ChatRepository(db));
            }
        }
        public CommentRepository Comments
        {
            get
            {
                return CommentRepository ?? (CommentRepository = new CommentRepository(db));
            }
        }
        public FriendRepository Friends
        {
            get
            {
                return FriendRepository ?? (FriendRepository = new FriendRepository(db));
            }
        }
        public MessageRepository Messages
        {
            get
            {
                return MessageRepository ?? (MessageRepository = new MessageRepository(db));
            }
        }
        public PostRepository Posts
        {
            get
            {
                return PostRepository ?? (PostRepository = new PostRepository(db));
            }
        }
        public QuestionRepository Questions
        {
            get
            {
                return QuestionRepository ?? (QuestionRepository = new QuestionRepository(db));
            }
        }
        public ResultRepository Results 
        {
            get
            {
                return ResultRepository ?? (ResultRepository = new ResultRepository(db));
            }
        }
        public TestRepository Tests
        {
            get
            {
                return TestRepository ?? (TestRepository = new TestRepository(db));
            }
        }
        public UserProfileRepository UserProfiles
        {
            get
            {
                return UserProfileRepository ?? (UserProfileRepository = new UserProfileRepository(db));
            }
        }
        public SubscriberRepository Subscribers
        {
            get
            {
                return SubscriberRepository ?? (SubscriberRepository = new SubscriberRepository(db));
            }
        }
        public PhotoRepository Photos
        {
            get
            {
                return PhotoRepository ?? (PhotoRepository = new PhotoRepository(db));
            }
        }
        public LikeRepository Likes
        {
            get
            {
                return LikeRepository ?? (LikeRepository = new LikeRepository(db));
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
