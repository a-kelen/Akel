using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Akel.Domain.Core;
using Akel.Infrastructure.Services;
using XUnitTestAkel.Mocks;
using Akel.Infrastructure.Data;
using System.Linq;

namespace XUnitTestAkel.Repositories
{
    public class SubscriberServiceTests
    {
        [Fact]
        public void CreateTest()
        {
            Subscriber subscriber = new Subscriber()
            {
                Id = new Guid()
            };
            var unit = new MockUnitOfWork();
            unit.MockRepository();
            unit.MockCreateForRep(subscriber);
            var service = new SubscriberService(unit.Object);

            var res = service.Create(subscriber).GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.IsAssignableFrom<Subscriber>(res);
        }
        [Fact]
        public void GetTest()
        {
            var list = new List<Subscriber>()
            {
            new Subscriber()
                {
                    Id = new Guid()
                }
            };
            var unit = new MockUnitOfWork();
            unit.MockRepository();
            unit.MockGetAllForRep(list);
            var service = new SubscriberService(unit.Object);

            var res = service.Get().GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.IsAssignableFrom<IEnumerable<Subscriber>>(res);
        }
        [Fact]
        public void DeleteTest()
        {
            var id = new Guid();
            Subscriber subscriber = new Subscriber()
            {
                Id = id
            };
            var unit = new MockUnitOfWork();
            unit.MockRepository();
            unit.MockGetForRep(subscriber);
            unit.MockDeleteForRep(subscriber);
            var service = new SubscriberService(unit.Object);

            var res = service.Delete(id).GetAwaiter().GetResult();

           // Assert.NotNull(res);
            Assert.Equal(id, subscriber.Id);
            Assert.IsAssignableFrom<Subscriber>(res);
        }
        [Fact]
        public void GetByIdTest()
        {
            var id = new Guid();
            Subscriber subscriber = new Subscriber()
            {
                Id = id
            };
            var unit = new MockUnitOfWork();
            unit.MockRepository();
            unit.MockGetForRep(subscriber);
            var service = new SubscriberService(unit.Object);

            var res = service.GetById(id).GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.Equal(id, subscriber.Id);
            Assert.IsAssignableFrom<Subscriber>(res);
        }
        [Fact]
        public void GetByUserTest()
        {
            var id = new Guid();
            var list = new List<Subscriber>()
            {
                new Subscriber()
                {
                    Id = new Guid(),
                    UserProfileId = id
                    
                },
                new Subscriber()
                {
                    Id = new Guid(),
                    UserProfileId = new Guid()
                }
            };
            var unit = new MockUnitOfWork();
            unit.MockRepository();
            unit.MockGetAllForRep(list);
            var service = new SubscriberService(unit.Object);

            var res = service.GetByUser(id).GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.NotEmpty(res);
            Assert.Equal(id, res.First().UserProfileId);
            Assert.IsAssignableFrom<IEnumerable<Subscriber>>(res);
        }
        [Fact]
        public void UpdateTest()
        {
            var id = new Guid();
            Subscriber subscriber = new Subscriber()
            {
                Id = id
            };
            var unit = new MockUnitOfWork();
            unit.MockRepository();
            unit.MockUpdateForRep(subscriber);
            var service = new SubscriberService(unit.Object);
            service.Update(subscriber).GetAwaiter().GetResult();
            unit.Verify(x => x.Subscribers.Update(subscriber));
        }

    }
}
