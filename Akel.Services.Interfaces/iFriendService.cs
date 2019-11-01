using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akel.Domain.Core;
using Microsoft.AspNetCore.Authorization;

namespace Akel.Services.Interfaces
{
    public interface iFriendService
    {
       
         Task AddFriend(UserProfile user, UserProfile friend);

      
         Task DeleteFriend(Friend friend);

         Task<IEnumerable<Friend>> GetFriends(UserProfile user);
        
        

    }
}
