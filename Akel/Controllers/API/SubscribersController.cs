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
    public class SubscribersController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly iSubscriberService subscriberService;

        public SubscribersController(iSubscriberService service)
        {
            _context = new UnitOfWork();
            subscriberService = service;
        }

        // GET: api/Subscribers
        [HttpGet]
        public async Task<IEnumerable<Subscriber>> GetSubscribers()
        {
            return await subscriberService.Get();
        }

        // GET: api/Subscribers/5
        [HttpGet("byuser/{id}")]
        public async Task<IEnumerable<Subscriber>> GetSubscriberByUser(Guid id)
        {
           
            return await subscriberService.GetByUser(id);
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<Subscriber>> GetSubscriber(Guid id)
        {
            var subscriber = await subscriberService.GetById(id);

            if (subscriber == null)
            {
                return NotFound();
            }

            return subscriber;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscriber(Guid id, Subscriber subscriber)
        {
            if (id != subscriber.Id)
            {
                return BadRequest();
            }

            await subscriberService.Update(subscriber);

            try
            {
                await subscriberService.Save();
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
        [HttpPost]
        public async Task<IActionResult> PostSubscriber(Subscriber subscriber)
        {
            subscriber = await subscriberService.Create(subscriber);

            return CreatedAtAction("GetSubscriber", new { id = subscriber.Id }, subscriber);
        }

        // DELETE: api/Subscribers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subscriber>> DeleteSubscriber(Guid id)
        {
            var subscriber = await subscriberService.Delete(id);

            return subscriber;
        }

        private bool SubscriberExists(Guid id)
        {
            return subscriberService.GetById(id) == null;
        }
    }
}
