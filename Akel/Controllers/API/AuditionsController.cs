using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditionsController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<AuditionsController> _logger;
        
        private IHostingEnvironment _appEnvironment;

        public AuditionsController(ApplContext context, ILogger<AuditionsController> logger, IHostingEnvironment appEnvironment)
        {
            _context = new UnitOfWork();
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        // GET: api/Auditions
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audition>>> GetAuditions()
        {
            var res =  await _context.Auditions.GetAll();
            return Ok(res);
        }

        public class AuditionVM
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid UserProfileId { get; set; }
            public bool IsSubscribe { get; set; }
            public int PostCount { get; set; }
        }
        [HttpGet("byuser/{id}")]
        public async Task<ActionResult<IEnumerable<AuditionVM>>> GetAuditions(Guid id)
        {
            
            var res = (await _context.Auditions.GetAll()).Where(x => x.UserProfileId == id).ToList();
            return Ok(res);
        }
        [HttpGet("byowner/{id}")]
        public async Task<ActionResult<IEnumerable<Audition>>> GetAuditionsOwn( Guid id)
        {
            var res = (await _context.Auditions.GetAll()).Where(x=>x.UserProfileId == id);
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
            Chat chat = new Chat();
            chat.AuditionId = audition.Id;
            await _context.Chats.Create(chat);
            await _context.Save();

            return CreatedAtAction("GetAudition", new { id = audition.Id }, audition);
        }
        [HttpPost("subscribe/{id}/{userId}")]
        public async Task<ActionResult<Audition>> PostAudition(Guid id , Guid userId)
        {
            _logger.LogInformation(id.ToString());
            _logger.LogInformation(userId.ToString());
            Subscriber subscriber = (await _context.Subscribers.GetAll()).FirstOrDefault(x => x.AuditionId == id && x.UserProfileId == userId);
            if(subscriber==null)
            {
                subscriber = new Subscriber { AuditionId = id, UserProfileId = userId };
                await _context.Subscribers.Create(subscriber);
                await _context.Save();
                return Ok(subscriber);
            } else
            {
                await _context.Subscribers.Delete(subscriber.Id);
                await _context.Save();
                return Ok(false);
            }

        }
        [HttpPost("addphoto/{id}")]
        public async Task<ActionResult> AddPhoto([FromRoute]Guid id, IFormFile file)
        {

            string path = "/photos/" + Guid.NewGuid().ToString() + "_" + Request.Form.Files[0].FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await Request.Form.Files[0].CopyToAsync(fileStream);
            }
            Audition a = await _context.Auditions.Get(id);
            a.Photo = path;
            await _context.Auditions.Update(a);
            await _context.Save();

            return Ok(a);
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
