using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Domain.Core
{
    public class User:IdentityUser 
    {
        public string qwerty { get; set; }
    }
}
