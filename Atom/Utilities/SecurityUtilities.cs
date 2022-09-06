using Atom.Data.Model.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Atom.Utilities
{
    public class SecurityUtilities : BaseUtilities, ISecurityUtilities
    {
        public class LoginAttemptResult
        {
            public enum LoginResult
            {
                Success = 1,
                UserNotFound = 2
            }

            public UserModel User;

            public LoginResult Result { get; set; }

            public IEnumerable<RoleModel> Roles { get; set; }

            public LoginAttemptResult(LoginResult loginResult, UserModel user, IEnumerable<RoleModel> roles)
            {
                User = user;
                Result = loginResult;
                Roles = roles;
            }
        }

        #region Constructors
        public SecurityUtilities(IServiceProvider provider) : base(provider) { }
        #endregion

        public LoginAttemptResult LoginUser(string email, string password, bool externalAuth = false)
        {
            UserModel user = SecurityManager.LoginUser(email, password, externalAuth);

            if(user == null)
            {
                return new LoginAttemptResult(LoginAttemptResult.LoginResult.UserNotFound, null, null);
            }
            else
            {
                IEnumerable<RoleModel> roles = SecurityManager.GetRoles(user.UserId);
                return new LoginAttemptResult(LoginAttemptResult.LoginResult.Success, user, roles);
            }
        }
    }
}
