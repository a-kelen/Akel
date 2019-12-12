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
    public class UserProfileService : iUserProfileService
    {
        private readonly UnitOfWork _context;
        public UserProfileService()
        {
            _context = new UnitOfWork();
        }

        public async Task<IEnumerable<UserProfile>> Get()
        {
            return await _context.UserProfiles.GetAll();
        }

        public async Task<IEnumerable<UserProfile>> Search(string searchedUser)
        {
            IEnumerable<UserProfile> users = ((await _context.UserProfiles.GetAll()));
            if (!String.IsNullOrEmpty(searchedUser))
                users = users.Where(p => p.FirstName.Contains(searchedUser) || p.LastName.Contains(searchedUser));
            return users;
        }

        public async Task<UserProfile> GetById(Guid id)
        {
            return await _context.UserProfiles.Get(id); 
        }

        public async Task<UserProfile> Create(UserProfile userProfile)
        {
            await _context.UserProfiles.Create(userProfile);
            await _context.Save();
            return userProfile;
        }

        public async Task Update(UserProfile userProfile)
        {
            await _context.UserProfiles.Update(userProfile);
        }

        public async Task<UserProfile> Delete(Guid id)
        {
            var userProfile = await _context.UserProfiles.Get(id);
            if (userProfile == null)
            {
                return null;
            }
            await _context.UserProfiles.Delete(userProfile.Id);
            await _context.Save();

            return userProfile;
        }

        public async Task Save()
        {
            await _context.Save();
        }
    }
}
