using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Domain.Core
{
    public class Temp
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
