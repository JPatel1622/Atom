using Atom.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using static Atom.Utilities.SecurityUtilities;
using Atom.Data.Model.Security;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Atom.Domain.Extension;
using Atom.Domain.Enum;

namespace Atom.Controllers
{
    [Route("api/security/[action]")]
    [ApiController]
    [RequireHttps]
    [ResponseCache(NoStore = true, Duration = 0)]
    public class SecurityController : APIControllerBase
    {
        public SecurityController(IServiceProvider provider) : base(provider){}

        // POST: api/security/LoginUser
        [HttpPost]
        public JsonResult LoginUser([FromBody]User user)
        {
            LoginAttemptResult attempt = SecurityUtility.LoginUser(user.Email, user.Password);

            switch (attempt.Result)
            {
                case LoginAttemptResult.LoginResult.Success:
                    AddCookieToUser(attempt);
                    break;
                case LoginAttemptResult.LoginResult.UserNotFound:
                    break;
            }

            return new JsonResult(JsonConvert.SerializeObject(attempt.Result));
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Duration = 0)]
        public string LoginUserString(string jsonData)
        {
            User user = JsonConvert.DeserializeObject<User>(jsonData);


            //SecurityUtility.LoginUser(user.Email, user.Password);

            return "jsonData";
        }

        // POST: api/security/SaveFavorite
        [HttpPost]
        public void SaveFavorite([FromBody] EventFavorite favorite)
        {
            EventType eventType = Enum.Parse<EventType>(favorite.EventType);

            SecurityManager.AddFavoriteEvent(CurrentUser, eventType, favorite.EventID);
        }

        [HttpPost]
        public void RemoveFavorite([FromBody] EventFavorite favorite)
        {
            EventType eventType = Enum.Parse<EventType>(favorite.EventType);

            SecurityManager.RemoveFavoriteEvent(CurrentUser, eventType, favorite.EventID);
        }

        private void AddCookieToUser(LoginAttemptResult attempt)
        {
            var claims = new List<Claim>();

            foreach(var role in attempt.Roles)
            {
                Claim claim = new Claim(ClaimTypes.Role, role.RoleTypeId.Description());
                claims.Add(claim);
            }

            Claim userClaim = new Claim(nameof(UserModel), JsonConvert.SerializeObject(attempt.User));

            claims.Add(userClaim);

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }

    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string email, string? password)
        {
            Email = email;
            Password = password;
        }
    }

    public class EventFavorite
    {
        public string EventType { get; set; }

        public string EventID { get; set; }
    }
}
