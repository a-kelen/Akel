using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Domain.Core
{
    public class ChangePasswordVM
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
