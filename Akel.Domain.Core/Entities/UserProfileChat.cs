using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Domain.Core
{
    public class UserProfileChat
    {
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
