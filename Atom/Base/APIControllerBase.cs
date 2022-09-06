using Atom.Data.Manager;
using Atom.Data.Model.Security;
using Atom.Extensions.HttpContext;
using Atom.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Atom.Base
{
    public class APIControllerBase : ControllerBase
    {
        #region Managers
        protected ISecurityManager SecurityManager { get; init; }
        protected ISecurityUtilities SecurityUtility { get; init; }
        protected ICSSManager CSSManager { get; init; }
        protected HttpClient HttpClient { get; init; }
        protected UserModel CurrentUser 
        { 
            get
            {
                return HttpContext.GetUserModel();
            }
        }
        #endregion

        protected APIControllerBase(IServiceProvider provider)
        {
            SecurityManager = provider.GetService<ISecurityManager>();
            SecurityUtility = provider.GetService<ISecurityUtilities>();
            CSSManager = provider.GetService<ICSSManager>();
            HttpClient = provider.GetService<IHttpClientFactory>().CreateClient();
        }
    }
}
