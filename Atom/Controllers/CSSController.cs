using Atom.Base;
using Atom.Data.Manager;
using Atom.Data.Model.Security;
using Atom.Extensions.HttpContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1

namespace Atom.Controllers
{
    [Route("api/css/[action]")]
    [ApiController]
    [RequireHttps]
    public class CSSController : APIControllerBase
    {
        #region Constructor
        public CSSController(IServiceProvider provider) : base(provider) { }
        #endregion

        protected UserModel? LoggedInUser => base.HttpContext.GetUserModel();
        protected bool IsLoggedIn => base.HttpContext.User.Identity.IsAuthenticated;

        // GET: api/css/GetUsers
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = SecurityManager.GetUserModels();

            if (users.Any())
            {
                StringBuilder sb = new StringBuilder();

                foreach (var user in users)
                {
                    sb.AppendLine($"{user.FirstName} {user.LastName}");
                }

                return Ok(sb.ToString());
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/css/CSSRules
        [HttpGet]
        public ContentResult CSSRules()
        {
            int userId;
            
            if(IsLoggedIn)
            {
                userId = LoggedInUser.UserId;
            } else
            {
                userId = 0;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(":root {");
            sb.AppendLine("--layoutBackgroundColor: " + CSSManager.GetCSS(userId).First(bgColor => bgColor.CSSVariableId == Domain.Enum.CSSVariableEnum.BackgroundColor).CSSVariableValue + ";");
            sb.AppendLine("--footerColor: " + CSSManager.GetCSS(userId).First(footerColor => footerColor.CSSVariableId == Domain.Enum.CSSVariableEnum.FooterColor).CSSVariableValue + ";");
            sb.AppendLine("--textColor: " + CSSManager.GetCSS(userId).First(textColor => textColor.CSSVariableId == Domain.Enum.CSSVariableEnum.TextColor).CSSVariableValue + ";");
            sb.AppendLine("--primaryBtnColor: " + CSSManager.GetCSS(userId).First(buttonColor => buttonColor.CSSVariableId == Domain.Enum.CSSVariableEnum.ButtonColor).CSSVariableValue + ";");
            sb.AppendLine("}");

            return Content(sb.ToString(), "text/css");
        }

        // POST api/<CSSController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CSSController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
