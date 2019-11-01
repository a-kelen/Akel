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
    public class AuditionsController : ControllerBase
    {
        private readonly UnitOfWork _context;

        public AuditionsController(ApplContext context)
        {
            _context = new UnitOfWork();
        }

        // GET: api/Auditions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audition>>> GetAuditions()
        {
            var res =  await _context.Auditions.GetAll();
            return Ok(res);
        }

        // GET: api/Auditions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Audition>> GetAudition(Guid id)
        {
            var audition = await _context.Auditions.Get(id);

            if (audition == null)
            {
                return NotFound();
            }

            return audition;
        }

        // PUT: api/Auditions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAudition(Guid id, Audition audition)
        {
            if (id != audition.Id)
            {
                return BadRequest();
            }

            await _context.Auditions.Update(audition);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditionExists(id))
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

        // POST: api/Auditions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Audition>> PostAudition(Audition audition)
        {
            await _context.Auditions.Create(audition);
            await _context.Save();

            return CreatedAtAction("GetAudition", new { id = audition.Id }, audition);
        }

        // DELETE: api/Auditions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Audition>> DeleteAudition(Guid id)
        {
            var audition = await _context.Auditions.Get(id);
            if (audition == null)
            {
                return NotFound();
            }

            await _context.Auditions.Delete(audition.Id);
            await _context.Save();

            return audition;
        }

        private bool AuditionExists(Guid id)
        {
            return _context.Auditions.Get(id) == null;
        }
    }
}
