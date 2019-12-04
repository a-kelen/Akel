using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<UserProfilesController> logger;
        private UserManager<User> userManager;


        public UserProfilesController(ApplContext context, UserManager<User> userManager, ILogger<UserProfilesController> l)
        {
            _context = new UnitOfWork();
            logger = l;
            this.userManager = userManager;
        }

        // GET: api/UserProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            return Ok(await _context.UserProfiles.GetAll());
        }
        
        [HttpGet("search/{searchedUser}")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetFilteredUserProfiles(string searchedUser)
        {
            IEnumerable<UserProfile> users = ((await _context.UserProfiles.GetAll()));
                    
            if (!String.IsNullOrEmpty(searchedUser))
                users =  users.Where(p => p.FirstName.Contains(searchedUser) || p.LastName.Contains(searchedUser));
            return Ok(users);
        }

        // GET: api/UserProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(Guid id)
        {   
            var userProfile = await _context.UserProfiles.Get(id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return userProfile;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile(Guid id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            await _context.UserProfiles.Update(userProfile);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/UserProfiles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.

        public class ProfileCreateVM
        {
            public string userId { get; set; }
            public string firstName { get; set; }
            public DateTime birthday { get; set; }
            public string lastName { get; set; }
            public bool sex { get; set; }
        }
        [HttpPost]
        public async Task<ActionResult<UserProfile>> PostUserProfile([FromBody] ProfileCreateVM vM)
        {
            UserProfile userProfile = new UserProfile
            {
                UserId = vM.userId,
                LastName = vM.lastName,
                FirstName = vM.firstName,
                Birthday = vM.birthday,
                Sex = vM.sex
            };
            logger.LogInformation("sdfsdf");
            await _context.UserProfiles.Create(userProfile);
            await _context.Save();
           
            return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile);
        }
        public class ChangePasswordVM
        {
            public string Username { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }
        [HttpPost("changepassword")]
        public async Task<ActionResult<UserProfile>> ChangePassword([FromBody] ChangePasswordVM vM)
        {
            User user = await userManager.FindByEmailAsync(vM.Username);
            var _passwordValidator =
                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
            var Hasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

            User tempU = new User();

            bool res = false;
            if (user != null)
                res = await userManager.CheckPasswordAsync(user, vM.OldPassword);
            
            if(res)
            {
                IdentityResult result =
                await _passwordValidator.ValidateAsync(userManager, user, vM.NewPassword);
                if (result.Succeeded)
                {
                    user.PasswordHash = Hasher.HashPassword(user, vM.NewPassword);
                    await userManager.UpdateAsync(user);
                    return Ok(true);
                }
                else return Ok(false);
            }
            else return Ok(false);


        }

        // DELETE: api/UserProfiles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserProfile>> DeleteUserProfile(Guid id)
        {
            var userProfile = await _context.UserProfiles.Get(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            await _context.UserProfiles.Delete(userProfile.Id);
            await _context.Save();

            return userProfile;
        }

        private bool UserProfileExists(Guid id)
        {
            return _context.UserProfiles.Get(id) == null;
        }
    }
}
