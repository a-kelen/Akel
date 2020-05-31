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
using Microsoft.AspNetCore.Authorization;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly iResultService resultService;
        public ResultsController(ApplContext context , iResultService service)
        {
            resultService = service;
            _context = new UnitOfWork();
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return Ok(await resultService.Get());
        }
        [HttpGet("byuser/{id}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByUser(Guid id)
        {
            
            return Ok(await resultService.GetByUser(id));
        }

        // GET: api/Results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(Guid id)
        {
            var result = await resultService.GetById(id);

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

            await resultService.Update(result);

            try
            {
                await resultService.Save();
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

            return Ok();
        }

        // POST: api/Results
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            result = await resultService.Create(result);

            return CreatedAtAction("GetResult", new { id = result.Id }, result);
        }

        // DELETE: api/Results/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteResult(Guid id)
        {
            var result = await resultService.Delete(id);

            return result;
        }

        private bool ResultExists(Guid id)
        {
            return _context.Results.Get(id) == null;
        }
    }
}
