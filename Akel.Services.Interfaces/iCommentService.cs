using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iCommentService
    {
        Task<IEnumerable<Comment>> Get();
        Task<Comment> GetById(Guid id);
        Task<Comment> Create(Comment chat);
        Task Update(Comment chat);
        Task<Comment> Delete(Guid id);
        Task Save();
    }
}
