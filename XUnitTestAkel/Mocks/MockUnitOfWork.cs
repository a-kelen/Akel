using Akel.Domain.Core;
using Akel.Domain.Interface;
using Akel.Infrastructure.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using XUnitTestAkel.Mocks.Repositories;

namespace XUnitTestAkel.Mocks
{
    public class MockUnitOfWork : Mock<iUnitOfWork>
    {
        ApplContext ctx = new ApplContext();
        MockSubscriberRepository rep = new MockSubscriberRepository();
        public MockUnitOfWork MockRepository()
        {
            Setup(x => x.Subscribers).Returns(rep.Object as IRepository<Subscriber>);
            return this;
        }
        public MockUnitOfWork MockCreateForRep(Subscriber subscriber)
        {
            rep.MockCreate(subscriber);
            return this;
        }
        public MockUnitOfWork MockGetForRep(Subscriber subscriber)
        {
            rep.MockGet(subscriber);
            return this;
        }
        public MockUnitOfWork MockGetAllForRep(IEnumerable<Subscriber> subscribers)
        {
            rep.MockGetAll(subscribers);
            return this;
        }
        public MockUnitOfWork MockDeleteForRep(Subscriber subscriber)
        {
            rep.MockDelete(subscriber);
            return this;
        }
        public MockUnitOfWork MockUpdateForRep(Subscriber subscriber)
        {
            rep.MockUpdate(subscriber);
            return this;
        }
    }
}
