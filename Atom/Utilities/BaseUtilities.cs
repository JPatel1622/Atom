using Atom.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Atom.Utilities 
{
    public class BaseUtilities
    {
        protected ISecurityManager SecurityManager { get; init; }
        protected IHttpContextAccessor HttpContextAccessor { get; init; }
        protected HttpContext Context { get; init; }

        #region Constructors
        public BaseUtilities(IServiceProvider provider)
        {
            SecurityManager = provider.GetService<ISecurityManager>();

            HttpContextAccessor = provider.GetService<IHttpContextAccessor>();

            Context = HttpContextAccessor.HttpContext;
        }
        #endregion
    }


}
