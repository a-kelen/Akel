using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iAuditionService
    {
        Task<IEnumerable<Audition>> Get();
        Task<Audition> GetById(Guid id);
        Task<IEnumerable<Audition>> GetByOwner(Guid id);
        Task<Audition> Create( Audition audition);
        Task Update(Audition audition);
        Task<Subscriber> Subscribe(Guid id, Guid userId);
        Task<Audition> AddPhoto(Guid id, string path);
        Task<Audition> Delete(Guid id);
        Task Save();
    }
}
