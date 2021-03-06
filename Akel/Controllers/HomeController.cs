using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Akel.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Akel.Infrastructure.Data;
using Akel.Domain.Core;
using Akel.Infrastructure.Services;
using Akel.Services.Interfaces;

namespace Akel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> userManager;
        private readonly UnitOfWork context;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, iFriendService friendService)
        {
            _logger = logger;
            this.userManager = userManager;
            context = new UnitOfWork();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tokens()
        {
            return View();
        }
        public IActionResult Front()
        {
            var id = userManager.GetUserId(HttpContext.User);
            _logger.LogInformation(id);
            return Redirect("http://localhost:8080/#/sign");
        }
        public IActionResult Chats()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public class TokenVM
        {
            public string grant_type { get; set; }
            public string username { get; set; }
            public string password { get; set; }

        }

        [HttpPost("/token")]
        public async Task Token([FromBody]TokenVM token)
        {
           
            
            var username = token.username;
            var password = token.password;
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            // сериализация ответа

            var user = await userManager.FindByEmailAsync(identity.Name);
            var profile = (await context.UserProfiles.GetAll()).FirstOrDefault(x => x.UserId == user.Id);
            var response = new
            {
                access_token = encodedJwt,
                username = user,
                profile = profile
            };

           
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            User user = await userManager.FindByEmailAsync(username);

            var Hasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

            User tempU = new User();

            bool res = false;
            if(user!=null)
             res = await userManager.CheckPasswordAsync(user,password);

            if (res)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
