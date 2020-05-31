using Akel.Domain.Core;
using Akel.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestAkel.Mocks.Services
{
    public class MockSubscriberService : Mock<iSubscriberService>
    {
        public MockSubscriberService MockGet(IEnumerable<Subscriber> list)
        {
            Console.WriteLine(list.Count());
            Setup(x => x.Get())
                .Returns(Task.FromResult(list));
            return this;
        }
        public MockSubscriberService MockGetById(Subscriber subscriber)
        {
            Setup(x => x.GetById(It.IsAny<Guid>())).
                Returns(Task.FromResult(subscriber));
            return this;
        }
        public MockSubscriberService MockGetByUser(IEnumerable<Subscriber> list)
        {
            Setup(x => x.GetByUser(It.IsAny<Guid>())).
                Returns(Task.FromResult(list));
            return this;
        }
        public MockSubscriberService MockPutSubscriber(Subscriber subscriber)
        {
            Setup(x => x.Update(It.IsAny<Subscriber>())).Returns(Task.FromResult(subscriber));
            return this;
        }
        public MockSubscriberService MockPostSubscriber(Subscriber subscriber)
        {
            Setup(x => x.Create(It.IsAny<Subscriber>())).Returns(Task.FromResult(subscriber));
            return this;
        }
        public MockSubscriberService MockDeleteSubscriber(Subscriber subscriber)
        {
            Setup(x => x.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(subscriber));
            return this;
        }
    }
}
