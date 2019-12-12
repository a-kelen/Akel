using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iResultService
    {
        Task<IEnumerable<Result>> Get();
        Task<IEnumerable<Result>> GetByUser(Guid id);
        Task<Result> GetById(Guid id);
        Task<Result> Create(Result result);
        Task Update(Result result);
        Task<Result> Delete(Guid id);
        Task Save();
    }
}
