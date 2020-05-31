using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akel.Domain.Core;
using Akel.Domain.Interface;
using Akel.Infrastructure.Data;
using Akel.Infrastructure.Services;
using Moq;
namespace XUnitTestAkel.Mocks.Repositories
{
    public class MockSubscriberRepository : Mock<IRepository<Subscriber>>
    {
        public MockSubscriberRepository MockCreate(Subscriber subscriber)
        {
            Setup(x => x.Create(It.IsAny<Subscriber>() ));
            return this;
        }
        public MockSubscriberRepository MockDelete(Subscriber subscriber)
        {
            Setup(x => x.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(subscriber));
            return this;
        }
        public MockSubscriberRepository MockGet(Subscriber subscriber)
        {
            Setup(x => x.Get(It.IsAny<Guid>())).Returns(Task.FromResult(subscriber));
            return this;
        }
        public MockSubscriberRepository MockGetAll(IEnumerable<Subscriber> list)
        {
            Setup(x => x.GetAll()).Returns(Task.FromResult(list));
            return this;
        }
        public MockSubscriberRepository MockUpdate(Subscriber subscriber)
        {
            Setup(x => x.Update(It.IsAny<Subscriber>() ));
            return this;
        }
        
    }
}
