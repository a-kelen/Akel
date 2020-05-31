using Akel.Domain.Core;
using Akel.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestAkel.Mocks.Services
{
    class MockTestService:Mock<iTestService>
    {
        public MockTestService MockGet(IEnumerable<Test> list)
        {
            Setup(x => x.Get())
                .Returns(Task.FromResult(list));
            return this;
        }
    }
}
