using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Akel.Controllers.API;
using XUnitTestAkel.Mocks.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Akel.Domain.Core;
using System.Linq;

namespace XUnitTestAkel.Controllers
{
    public class SubscriberControllerTests
    {
        [Fact]
        public void GetSubscribersTest()
        {
            var list = new List<Subscriber>() {
                   new Subscriber()
                   {
                       Id = new Guid()
                   },
                   new Subscriber()
                   {
                       Id = new Guid()
                   }
            };

            var subscriberService = new MockSubscriberService().MockGet(list);
            var controller = new SubscribersController(subscriberService.Object);

            var res = controller.GetSubscribers().GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.Equal(2, res.Count());
            Assert.IsAssignableFrom<IEnumerable<Subscriber>>(res);

        }
       

        [Fact]
        public void GetByIdTest()
        {
            var subscriber = new Subscriber()
            {
                Id = new Guid()
            };

            var subscriberService = new MockSubscriberService().MockGetById(subscriber);
            var controller = new SubscribersController(subscriberService.Object);

            var res = controller.GetSubscriber(new Guid()).GetAwaiter().GetResult();
            Assert.NotNull(res.Value);
            Assert.IsAssignableFrom<Subscriber>(res.Value);

        }
        [Fact]
        public void GetByUserTest()
        {
            var list = new List<Subscriber>();

            var subscriberService = new MockSubscriberService().MockGetByUser(list);
            var controller = new SubscribersController(subscriberService.Object);

            var res = controller.GetSubscriberByUser(new Guid()).GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.Empty(res);
            Assert.IsAssignableFrom<IEnumerable<Subscriber>>(res);

        }
        [Fact]
        public void PutSubscriberTest()
        {
            var id = new Guid();
            var subscriber = new Subscriber()
            {
                Id = id
            };

            var subscriberService = new MockSubscriberService().MockPutSubscriber(subscriber);
            var controller = new SubscribersController(subscriberService.Object);

            var res = controller.PutSubscriber(id , subscriber).GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.IsAssignableFrom<IActionResult>(res);

        }
        [Fact]
        public void PutSubscriberBadIdTest()
        {
            var id = new Guid("a746ce24-5052-4e8b-9771-94d02daafd8d");
            var subscriber = new Subscriber()
            {
                Id = new Guid("2a6ab632-efce-4181-9ee2-0e525bc8354f")
            };

            var subscriberService = new MockSubscriberService().MockPutSubscriber(subscriber);
            var controller = new SubscribersController(subscriberService.Object);

            var res = controller.PutSubscriber(id, subscriber).GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.IsAssignableFrom<BadRequestResult>(res);

        }
        [Fact]
        public void PostSubscriberTest()
        {
            var subscriber = new Subscriber()
            {
                Id = new Guid("2a6ab632-efce-4181-9ee2-0e525bc8354f")
            };

            var subscriberService = new MockSubscriberService()
                .MockPostSubscriber(subscriber)
                .MockGetById(subscriber);
            var controller = new SubscribersController(subscriberService.Object);

            var res = controller.PostSubscriber(subscriber).GetAwaiter().GetResult();
            
            Assert.NotNull(res);
            Assert.IsAssignableFrom<CreatedAtActionResult>(res);
            var obj = res as CreatedAtActionResult;
            Assert.IsAssignableFrom<Subscriber>(obj.Value);
            Assert.Equal(subscriber.Id, (obj.Value as Subscriber).Id);

        }
        [Fact]
        public void DeleteSubscriberTest()
        {
            var id = new Guid("a746ce24-5052-4e8b-9771-94d02daafd8d");
            var subscriber = new Subscriber()
            {
                Id = id
            };

            var subscriberService = new MockSubscriberService().MockDeleteSubscriber(subscriber);
            var controller = new SubscribersController(subscriberService.Object);

            var res = controller.DeleteSubscriber(id).GetAwaiter().GetResult();

            Assert.NotNull(res);
            Assert.IsAssignableFrom<Subscriber>(res.Value);

        }
    }
}
