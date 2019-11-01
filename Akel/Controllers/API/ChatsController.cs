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
    public class ChatsController : ControllerBase
    {
        private readonly UnitOfWork _context;

        public ChatsController(ApplContext context)
        {
            _context = new UnitOfWork();
        }

        // GET: api/Chats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        {
            var res =  await _context.Chats.GetAll();
            return Ok(res);
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(Guid id)
        {
            var chat = await _context.Chats.Get(id);

            if (chat == null)
            {
                return NotFound();
            }

            return chat;
        }

        // PUT: api/Chats/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(Guid id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            await _context.Chats.Update(chat);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
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

        // POST: api/Chats
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(Chat chat)
        {
            await _context.Chats.Create(chat);
            await _context.Save();

            return CreatedAtAction("GetChat", new { id = chat.Id }, chat);
        }

        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chat>> DeleteChat(Guid id)
        {
            var chat = await _context.Chats.Get(id);
            if (chat == null)
            {
                return NotFound();
            }

            await _context.Chats.Delete(chat.Id);
            await _context.Save();

            return chat;
        }

        private bool ChatExists(Guid id)
        {
            return _context.Chats.Get(id) == null;    
        }
    }
}
