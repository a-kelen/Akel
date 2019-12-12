using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iChatService
    {
        Task<IEnumerable<Chat>> Get();
        Task<Chat> GetById(Guid id);
        Task<Chat> Create(Chat chat);
        Task Update(Chat chat);
        Task<Chat> Delete(Guid id);
        Task Save();
    }
}
