using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Akel.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Akel.Infrastructure.Services
{
    public class FriendService : iFriendService
    {
        UnitOfWork unit;
        public FriendService(UnitOfWork unit)
        {
            this.unit = unit;
        }
        [Authorize]
        public async Task AddFriend(UserProfile user, UserProfile friend)
        {
            Friend newFriend = new Friend {UserProfileId = user.Id, UserFriendId = user.Id };
            await unit.Friends.Create(newFriend);
            await unit.Save();
        }
        [Authorize]
        public async Task DeleteFriend(Friend friend)
        {
            await unit.Friends.Delete(friend.Id);
            await unit.Save();
        }
        [Authorize]
        public async Task<IEnumerable<Friend>> GetFriends(UserProfile user)
        {
            var res = await unit.Friends.GetAll();
            
           return  res;
        }
    }
}
