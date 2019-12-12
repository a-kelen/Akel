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
    public class ResultService : iResultService
    {
        private readonly UnitOfWork _context;
        public ResultService()
        {
            _context = new UnitOfWork();
        }

        public async Task<Result> Create(Result result)
        {
            await _context.Results.Create(result);
            await _context.Save();
            return result;
        }

        public async Task<Result> Delete(Guid id)
        {
            var result = await _context.Results.Get(id);
            if (result == null)
            {
                return null;
            }

            await _context.Results.Delete(result.Id);
            await _context.Save();

            return result;
        }

        public async Task<IEnumerable<Result>> Get()
        {
            return await _context.Results.GetAll();
        }

        public async Task<Result> GetById(Guid id)
        {
            return await _context.Results.Get(id);
        }

        public async Task<IEnumerable<Result>> GetByUser(Guid id)
        {
            var res = (await _context.Results.GetAll()).Where(x => x.UserProfileId == id);
            return res;
        }

        public async Task Save()
        {
            await _context.Save();
        }

        public async Task Update(Result result)
        {
            await _context.Results.Update(result);
        }
    }
}
