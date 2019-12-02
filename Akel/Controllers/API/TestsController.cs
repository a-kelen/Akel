using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Akel.Domain.Core;
using Akel.Infrastructure.Data;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly UnitOfWork _context;

        public TestsController(ApplContext context)
        {
            _context = new UnitOfWork();
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            return Ok(await _context.Tests.GetAll());
        }
        [HttpGet("byaudition/{id}")]
        public async Task<ActionResult<IEnumerable<Test>>> GetTests(Guid id)
        {
            var res = (await _context.Tests.GetAll()).Where(x => x.AuditionId == id);
            return Ok(res);
        }


        public class PagedCollectionResponse<T> where T : class
        {
            public IEnumerable<T> Items { get; set; }
            public Uri NextPage { get; set; }
            public Uri PreviousPage { get; set; }
        }
        IEnumerable<Test> tests = new List<Test>()
            {
                new Test {Id = new Guid(),Title = "Title_1", Topic = "Topic_1"},
                new Test {Id = new Guid(),Title = "Title_2", Topic = "Topic_2"},
                new Test {Id = new Guid(),Title = "Title_3", Topic = "Topic_3"},
                new Test {Id = new Guid(),Title = "Title_4", Topic = "Topic_4"},
                new Test {Id = new Guid(),Title = "Title_5", Topic = "Topic_5"},
                new Test {Id = new Guid(),Title = "Title_6", Topic = "Topic_6"},
                new Test {Id = new Guid(),Title = "Title_7", Topic = "Topic_7"},
                new Test {Id = new Guid(),Title = "Title_8", Topic = "Topic_8"},
                new Test {Id = new Guid(),Title = "Title_9", Topic = "Topic_9"},
                new Test {Id = new Guid(),Title = "Title_10", Topic = "Topic_10"},
                new Test {Id = new Guid(),Title = "Title_11", Topic = "Topic_11"}
            };
        [HttpGet("paging")]
        public async Task<ActionResult<PagedCollectionResponse<Test>>> GetTests([FromQuery] SampleFilterModel filter)
        {
            Func<SampleFilterModel, IEnumerable<Test>> filterData = (filterModel) =>
            {
                return tests.Skip((filterModel.Page - 1) * filter.Limit)
                .Take(filterModel.Limit);
            };

            var result = new PagedCollectionResponse<Test>();
            result.Items = filterData(filter);
            SampleFilterModel nextFilter = filter.Clone() as SampleFilterModel;
            nextFilter.Page += 1;
            String nextUrl = filterData(nextFilter).Count() <= 0 ? null : this.Url.Action("Get", null, nextFilter, Request.Scheme);

            //Get previous page URL string  
            SampleFilterModel previousFilter = filter.Clone() as SampleFilterModel;
            previousFilter.Page -= 1;
            String previousUrl = previousFilter.Page <= 0 ? null : this.Url.Action("Get", null, previousFilter, Request.Scheme);

            result.NextPage = !String.IsNullOrWhiteSpace(nextUrl) ? new Uri(nextUrl) : null;
            result.PreviousPage = !String.IsNullOrWhiteSpace(previousUrl) ? new Uri(previousUrl) : null;

            return Ok(result);
        }
        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(Guid id)
        {
            var test = await _context.Tests.Get(id);

            if (test == null)
            {
                return NotFound();
            }

            return test;
        }

        // PUT: api/Tests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(Guid id, Test test)
        {
            if (id != test.Id)
            {
                return BadRequest();
            }

            await _context.Tests.Update(test);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        public class CreateTestVM
        {
            public Guid AuditionId { get; set; }
            public string Title { get; set; }
            public string Topic { get; set; }
            public List<Question> Questions { get; set; }
            public CreateTestVM()
            {
                this.Questions = new List<Question>();
                
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Test>> PostTest(CreateTestVM vm)
        {

            Test test = new Test
            {
                AuditionId = vm.AuditionId,
                Title = vm.Title,
                Topic = vm.Topic,
                Questions = vm.Questions
            };
            await _context.Tests.Create(test);
            await _context.Save();
            foreach(var a in test.Questions)
            {
               // var c = a.Correct;
                

            }

            return CreatedAtAction("GetTest", new { id = test.Id }, test);
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Test>> DeleteTest(Guid id)
        {
            var test = await _context.Tests.Get(id);
            if (test == null)
            {
                return NotFound();
            }

            await _context.Tests.Delete(test.Id);
            await _context.Save();

            return test;
        }

        private bool TestExists(Guid id)
        {
            return _context.Tests.Get(id) == null;
        }
    }
}
