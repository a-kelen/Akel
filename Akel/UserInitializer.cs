using Akel.Domain.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akel
{
    public static class UserInitializer
    {
        public static void SeedData
    (UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers
    (UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                IdentityResult result = userManager.CreateAsync
                (user, "_123123Aa").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "admin").Wait();
                }
            }
        }

        public static void SeedRoles
    (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
