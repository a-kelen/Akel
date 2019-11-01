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
    public class FriendsController : ControllerBase
    {
        private readonly UnitOfWork _context;

        public FriendsController(ApplContext context)
        {
            _context = new UnitOfWork();
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriends()
        {
            return Ok(await _context.Friends.GetAll());
        }

        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friend>> GetFriend(Guid id)
        {
            var friend = await _context.Friends.Get(id);

            if (friend == null)
            {
                return NotFound();
            }

            return friend;
        }

        // PUT: api/Friends/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriend(Guid id, Friend friend)
        {
            if (id != friend.Id)
            {
                return BadRequest();
            }

            await  _context.Friends.Update(friend);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

        // POST: api/Friends
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Friend>> PostFriend(Friend friend)
        {
            await _context.Friends.Create(friend);
            await _context.Save();

            return CreatedAtAction("GetFriend", new { id = friend.Id }, friend);
        }

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friend>> DeleteFriend(Guid id)
        {
            var friend = await _context.Friends.Get(id);
            if (friend == null)
            {
                return NotFound();
            }

            await _context.Friends.Delete(friend.Id);
            await _context.Save();

            return friend;
        }

        private bool FriendExists(Guid id)
        {
            return _context.Friends.Get(id) == null;
        }
    }
}
