using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iTestService
    {
        Task<IEnumerable<Test>> Get();
        Task<IEnumerable<Test>> GetByAudition(Guid id);
        Task<PagedCollectionResponse<Test>> GetPage(SampleFilterModel filter);
        Task<Test> GetById(Guid id);
        Task<Test> Create(Test test);
        Task Update(Test test);
        Task<Test> Delete(Guid id);
        Task Save();
    }
}
