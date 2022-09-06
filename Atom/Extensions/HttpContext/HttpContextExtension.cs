using Atom.Data.Model.Security;
using Newtonsoft.Json;
using System.Linq;

namespace Atom.Extensions.HttpContext
{
    public static class HttpContextExtension
    {
        public static UserModel? GetUserModel(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            var claim = context.User.Claims.FirstOrDefault(x => x.Type == nameof(UserModel));

            if (claim != null)
            {
                UserModel user = JsonConvert.DeserializeObject<UserModel>(claim.Value);
                return user;
            }

            return null;
        }
    }
}
