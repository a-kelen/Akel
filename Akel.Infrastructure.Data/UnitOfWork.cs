﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Infrastructure.Data
{
    class UnitOfWork:IDisposable
    {
        AppContext db = new AppContext();
        AnswerRepository AnswerRepository;
        AuditionRepository AuditionRepository;
        ChatRepository ChatRepository;
        CommentRepository CommentRepository;
        FriendRepository FriendRepository;
        MemberRepository MemberRepository;
        MessageRepository MessageRepository;
        PostRepository PostRepository;
        QuestionRepository QuestionRepository;
        ResultRepository ResultRepository;
        TestRepository TestRepository;
        UserProfileRepository UserProfileRepository;

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
        public MemberRepository Members
        {
            get
            {
                return MemberRepository ?? (MemberRepository = new MemberRepository(db));
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

        public void Save()
        {
            db.SaveChanges();
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