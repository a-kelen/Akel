using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Akel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Services
{
    public class TestService : iTestService
    {
        private readonly iUnitOfWork _context;
        public TestService(iUnitOfWork unit)
        {
            _context = unit;
        }

        public async Task<Test> Create(Test test)
        {
            await _context.Tests.Create(test);
            await _context.Save();
            return test;
        }

        public async Task<Test> Delete(Guid id)
        {
            var test = await _context.Tests.Get(id);
            if (test == null)
            {
                return null;
            }

            await _context.Tests.Delete(test.Id);
            await _context.Save();

            return test;
        }

        public async Task<IEnumerable<Test>> Get()
        {
            return await _context.Tests.GetAll();
        }

        public async Task<IEnumerable<Test>> GetByAudition(Guid id)
        {
            var res = (await _context.Tests.GetAll()).Where(x => x.AuditionId == id);
            return res;
        }

        public async Task<Test> GetById(Guid id)
        {
            return await _context.Tests.Get(id); 
        }

        public async  Task<PagedCollectionResponse<Test>> GetPage(SampleFilterModel filter)
        {
            var tests = await _context.Tests.GetAll();
            Func<SampleFilterModel, IEnumerable<Test>> filterData = (filterModel) =>
            {
                return tests.Skip((filterModel.Page - 1) * filter.Limit)
                .Take(filterModel.Limit);
            };

            var result = new PagedCollectionResponse<Test>();
            result.Items = filterData(filter);
            result.AllCount = tests.Count();
            SampleFilterModel nextFilter = filter.Clone() as SampleFilterModel;
            nextFilter.Page += 1;
            //String nextUrl = filterData(nextFilter).Count() <= 0 ? null : this.Url.Action("Get", null, nextFilter, Request.Scheme);

            //Get previous page URL string  
            SampleFilterModel previousFilter = filter.Clone() as SampleFilterModel;
            previousFilter.Page -= 1;
            //String previousUrl = previousFilter.Page <= 0 ? null : this.Url.Action("Get", null, previousFilter, Request.Scheme);

            //result.NextPage = !String.IsNullOrWhiteSpace(nextUrl) ? new Uri(nextUrl) : null;
            //result.PreviousPage = !String.IsNullOrWhiteSpace(previousUrl) ? new Uri(previousUrl) : null;
            return result;
        }

        public async Task Save()
        {
            await _context.Save();
        }

        public async Task Update(Test test)
        {
            await _context.Tests.Update(test);
        }
    }

}
