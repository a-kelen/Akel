using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akel.Domain.Core;
using Akel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Akel.Controllers.Templates
{
    [Route("api/[controller]")]
    public class AdminPanelController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<User> userManager;

        public AdminPanelController(UserManager<User> userm,RoleManager<IdentityRole> manager)
        {
            roleManager = manager;
            userManager = userm;
        }
        [HttpPost("/role")]
        public async Task<IActionResult> CreateRole ([FromBody]string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok();
                }
                return NotFound();
            }
            return NotFound();
        }
        [HttpPost("/edituser")]
        public async Task<IActionResult> EditUser([FromBody] UserRolesEditViewModel model)
        {

            User user = await userManager.FindByIdAsync(model.userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var addedRoles = model.roles.Except(userRoles);
                var removedRoles = userRoles.Except(model.roles);
                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.RemoveFromRolesAsync(user, removedRoles);

                return Ok();
            }

            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                return Ok();
            }
            return NotFound();
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Edit(string userId)
        {
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return Ok(model);
            }

            return NotFound();
        }
        
    }
}
