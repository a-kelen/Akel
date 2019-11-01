using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Akel.Infrastructure.Data.DTO;
using AutoMapper;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly IMapper mapper;
        public AnswersController(ApplContext context,IMapper mapper)
        {
            _context = new UnitOfWork();
            this.mapper = mapper;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAnswers()
        {
            var res = await _context.Answers.GetAll();
            var resDto = mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerDTO>>(res);
            return Ok(resDto);
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDTO>> GetAnswer(Guid id)
        {
            var answer = await _context.Answers.Get(id);
            
            if (answer == null)
            {
                return NotFound();
            }
            var answerDto = mapper.Map<Answer, AnswerDTO>(answer);
            return answerDto;
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(Guid id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            await _context.Answers.Update(answer);  

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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

        // POST: api/Answers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            await _context.Answers.Create(answer);
            await _context.Save();

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Answer>> DeleteAnswer(Guid id)
        {
            var answer = await _context.Answers.Get(id);
            if (answer == null)
            {
                return NotFound();
            }

            await _context.Answers.Delete(answer.Id);
            await _context.Save();

            return answer;
        }

        private bool AnswerExists(Guid id)
        {
            return (_context.Answers.Get(id) == null );
        }
    }
}
