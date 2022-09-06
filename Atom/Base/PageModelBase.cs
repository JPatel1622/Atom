using Atom.Data.Manager;
using Atom.Data.Model.Security;
using Atom.Domain.Attribute;
using Atom.Extensions.HttpContext;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Atom.Base
{
    public abstract class PageModelBase : PageModel
    {
        #region Abstract Methods
        #endregion

        protected UserModel? LoggedInUser => base.HttpContext.GetUserModel();

        public bool IsLoggedIn => base.HttpContext.User.Identity.IsAuthenticated;

        protected ISecurityManager SecurityManager { get; }
        //private IMemoryCache MemoryCache { get; }

        protected ICSSManager CSSManager { get; }

        public PageModelBase(IServiceProvider provider)
        {
            SecurityManager = provider.GetService<ISecurityManager>();
            CSSManager = provider.GetService<ICSSManager>();
            //MemoryCache = provider.GetService<IMemoryCache>(); 
        }

        ////after get
        //public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        //{

        //    var pageProps = this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.CreateInstance);
        //    List<PropertyInfo> managedProps = new();

        //    foreach (var prop in pageProps)
        //    {
        //        if (Attribute.IsDefined(prop, typeof(Manage)))
        //        {
        //            managedProps.Add(prop);
        //        }
        //    }

        //    if (managedProps.Count > 0)
        //    {
        //        foreach (var prop in managedProps)
        //        {

        //            MemoryCache.Set(prop.MetadataToken, prop);
        //        }
        //    }

        //    base.OnPageHandlerExecuted(context);
        //}

        ////before post
        //public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        //{
        //    var pageProps = this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.CreateInstance);
        //    List<PropertyInfo> managedProps = new();

        //    foreach (var prop in pageProps)
        //    {
        //        if (Attribute.IsDefined(prop, typeof(Manage)))
        //        {
        //            managedProps.Add(prop);
        //        }
        //    }

        //    if (managedProps.Count > 0)
        //    {
        //        foreach (var prop in managedProps)
        //        {
        //            MemoryCache.Set(prop.MetadataToken, prop);
        //        }
        //    }

        //    base.OnPageHandlerSelected(context);
        //}

        ////after post
        //public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        //{
        //    var pageProps = this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.CreateInstance);
        //    List<PropertyInfo> managedProps = new();

        //    foreach (var prop in pageProps)
        //    {
        //        if (Attribute.IsDefined(prop, typeof(Manage)))
        //        {
        //            managedProps.Add(prop);
        //        }
        //    }

        //    if (managedProps.Count > 0)
        //    {
        //        foreach (var prop in managedProps)
        //        {
        //            MemoryCache.Set(prop.MetadataToken, prop);
        //        }
        //    }

        //    base.OnPageHandlerExecuting(context);
        //}
    }
}
