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
    public class CabinetController : Controller
    {
        UserManager<User> userManager;
        public CabinetController(UserManager<User> manager)
        {
            userManager = manager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetInfo(Guid id)
        {
            User user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
          
            return Ok(user);
        }
        [HttpGet("roled/{id}")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles(Guid id)
        {
            User user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var res = await userManager.GetRolesAsync(user);
            return Ok(res);
        }

        // POST api/<controller>
        [HttpPost("/edit")]
        public async Task<ActionResult> Edit([FromBody]EditUserViewModel model)
        {
            User user = await userManager.FindByIdAsync(model.Id);
            if(user!=null)
            {
                user.Email = model.Email;
                var res = await userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return Ok();
                }
            }
            return NotFound();
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            User user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("/changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            
                User user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await userManager.UpdateAsync(user);
                        return Ok();
                    }
                return NotFound();
                }
                else
                {
                    return NotFound();
                }
        }
    }
}
