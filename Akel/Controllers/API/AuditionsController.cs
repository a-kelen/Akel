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
using Akel.Infrastructure.Services;
using Akel.Services.Interfaces;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditionsController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<AuditionsController> _logger;
        private readonly iAuditionService auditionService;

        private IHostingEnvironment _appEnvironment;

        public AuditionsController(ApplContext context,
            ILogger<AuditionsController> logger,
            IHostingEnvironment appEnvironment ,
            iAuditionService auditionService)
        {
            _context = new UnitOfWork();
            _logger = logger;
            _appEnvironment = appEnvironment;
            this.auditionService = auditionService;
        }

        // GET: api/Auditions
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audition>>> GetAuditions()
        {
           
            return Ok(await auditionService.Get());
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

            var res = await auditionService.GetByOwner(id);
            return Ok(res);
        }
        [HttpGet("byowner/{id}")]
        public async Task<ActionResult<IEnumerable<Audition>>> GetAuditionsOwn( Guid id)
        {
            var res = await auditionService.GetByOwner(id);
            return Ok(res);
        }

        // GET: api/Auditions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Audition>> GetAudition(Guid id)
        {
            var audition = await auditionService.GetById(id);

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

            await auditionService.Update(audition);

            try
            {
                await auditionService.Save();
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
            audition = await auditionService.Create(audition);

            return CreatedAtAction("GetAudition", new { id = audition.Id }, audition);
        }
        [HttpPost("subscribe/{id}/{userId}")]
        public async Task<ActionResult<Subscriber>> PostAudition(Guid id , Guid userId)
        {
            Subscriber subscriber = await auditionService.Subscribe(id, userId);
            if(subscriber!=null)
            {
                return Ok(subscriber);
            } else
            {
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
            Audition a = await auditionService.AddPhoto(id, path);

            return Ok(a);
        }
        // DELETE: api/Auditions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Audition>> DeleteAudition(Guid id)
        {

            Audition audition = await auditionService.Delete(id);
            return audition;
        }

        private bool AuditionExists(Guid id)
        {
            return _context.Auditions.Get(id) == null;
        }
    }
}
