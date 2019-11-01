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
    public class SubscribersController : ControllerBase
    {
        private readonly UnitOfWork _context;

        public SubscribersController(ApplContext context)
        {
            _context = new UnitOfWork();
        }

        // GET: api/Subscribers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscriber>>> GetSubscribers()
        {
            return Ok(await _context.Subscribers.GetAll());
        }

        // GET: api/Subscribers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetSubscriber(Guid id)
        {
            var subscriber = await _context.Subscribers.Get(id);

            if (subscriber == null)
            {
                return NotFound();
            }

            return subscriber;
        }

        // PUT: api/Subscribers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscriber(Guid id, Subscriber subscriber)
        {
            if (id != subscriber.Id)
            {
                return BadRequest();
            }

            await _context.Subscribers.Update(subscriber);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriberExists(id))
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

        // POST: api/Subscribers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Subscriber>> PostSubscriber(Subscriber subscriber)
        {
           await _context.Subscribers.Create(subscriber);
            await _context.Save();

            return CreatedAtAction("GetSubscriber", new { id = subscriber.Id }, subscriber);
        }

        // DELETE: api/Subscribers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subscriber>> DeleteSubscriber(Guid id)
        {
            var subscriber = await _context.Subscribers.Get(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            await _context.Subscribers.Delete(subscriber.Id);
            await _context.Save();

            return subscriber;
        }

        private bool SubscriberExists(Guid id)
        {
            return _context.Subscribers.Get(id) == null;
        }
    }
}
