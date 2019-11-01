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
    public class ResultsController : ControllerBase
    {
        private readonly UnitOfWork _context;

        public ResultsController(ApplContext context)
        {
            _context = new UnitOfWork();
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return Ok(await _context.Results.GetAll());
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(Guid id)
        {
            var result = await _context.Results.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Results/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(Guid id, Result result)
        {
            if (id != result.Id)
            {
                return BadRequest();
            }

           await _context.Results.Update(result);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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

        // POST: api/Results
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            await _context.Results.Create(result);
            await _context.Save();

            return CreatedAtAction("GetResult", new { id = result.Id }, result);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteResult(Guid id)
        {
            var result = await _context.Results.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            await _context.Results.Delete(result.Id);
            await _context.Save();

            return result;
        }

        private bool ResultExists(Guid id)
        {
            return _context.Results.Get(id) == null;
        }
    }
}
