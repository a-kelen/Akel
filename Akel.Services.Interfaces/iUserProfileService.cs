using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iUserProfileService
    {
        Task<IEnumerable<UserProfile>> Get();
        Task<IEnumerable<UserProfile>> Search(string searchedUser);
        Task<UserProfile> GetById(Guid id);
        Task<UserProfile> Create(UserProfile userProfile);
        Task Update(UserProfile userProfile);
        Task<UserProfile> Delete(Guid id);
        Task Save();
    }
}
