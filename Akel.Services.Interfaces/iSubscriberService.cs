using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iSubscriberService
    {
        Task<IEnumerable<Subscriber>> Get();
        Task<IEnumerable<Subscriber>> GetByUser(Guid id);
        Task<Subscriber> GetById(Guid id);
        Task<Subscriber> Create(Subscriber subscriber);
        Task Update(Subscriber subscriber);
        Task<Subscriber> Delete(Guid id);
        Task Save();
    }
}
