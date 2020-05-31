using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Akel.Services.Interfaces;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly iTestService testService;

        public TestsController(iTestService service)
        {
            testService = service;
            _context = new UnitOfWork();
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<IEnumerable<Test>> GetTests()
        {
            return await testService.Get();
        }
        [HttpGet("byaudition/{id}")]
        public async Task<ActionResult<IEnumerable<Test>>> GetTestsBy(Guid id)
        {
           
            return Ok(await testService.GetByAudition(id));
        }

        
        
        [HttpGet("paging")]
        public async Task<ActionResult<PagedCollectionResponse<Test>>> GetTests([FromQuery] SampleFilterModel filter)
        {

            var res = await testService.GetPage(filter);
            return Ok(res);
        }
        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(Guid id)
        {
            var test = await testService.GetById(id);

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

            await testService.Update(test);

            try
            {
                await testService.Save();
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
            test = await testService.Create(test);

            return CreatedAtAction("GetTest", new { id = test.Id }, test);
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Test>> DeleteTest(Guid id)
        {
            var test = await testService.Delete(id);
            return test;
        }

        private bool TestExists(Guid id)
        {
            return _context.Tests.Get(id) == null;
        }
    }
}
