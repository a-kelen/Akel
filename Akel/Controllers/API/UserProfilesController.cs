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
using Akel.Services.Interfaces;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private readonly ILogger<UserProfilesController> logger;
        private UserManager<User> userManager;
        private readonly iUserProfileService profileService;

        public UserProfilesController(ApplContext context,
            UserManager<User> userManager,
            ILogger<UserProfilesController> l,
            iUserProfileService service)
        {
            _context = new UnitOfWork();
            logger = l;
            this.userManager = userManager;
            profileService = service;
        }

        // GET: api/UserProfiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            return Ok( await profileService.Get());
        }
        
        [HttpGet("search/{searchedUser}")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetFilteredUserProfiles(string searchedUser)
        {
            return Ok(await profileService.Search(searchedUser));
        }

        // GET: api/UserProfiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(Guid id)
        {
            var userProfile = await profileService.GetById(id);

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

            await profileService.Update(userProfile);

            try
            {
                await profileService.Save();
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

            userProfile = await profileService.Create(userProfile);
           
            return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile);
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
            var userProfile = await profileService.Delete(id);
            return userProfile;
        }

        private bool UserProfileExists(Guid id)
        {
            return _context.UserProfiles.Get(id) == null;
        }
    }
}
