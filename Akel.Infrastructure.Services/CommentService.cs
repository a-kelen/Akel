using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Akel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Services
{
    public class CommentService : iCommentService
    {
        private readonly UnitOfWork _context;
        public CommentService()
        {
            _context = new UnitOfWork();
        }

        public async Task<Comment> Create(Comment comment)
        {
            await _context.Comments.Create(comment);
            await _context.Save();
            return comment;
        }

        public async Task<Comment> Delete(Guid id)
        {
            var comment = await _context.Comments.Get(id);
            
            await _context.Comments.Delete(comment.Id);
            await _context.Save();
            return comment;
        }

        public async Task<IEnumerable<Comment>> Get()
        {
            return await _context.Comments.GetAll();
        }

        public async Task<Comment> GetById(Guid id)
        {
            var comment = await _context.Comments.Get(id);
            return comment;
        }

        public async Task Save()
        {
            await _context.Save();
        }

        public async Task Update(Comment comment)
        {
            await _context.Comments.Update(comment);
        }
    }
}
