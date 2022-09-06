using Atom.Data.Configurations;
using Atom.Data.Manager;
using Atom.Domain.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Atom.Domain.Enum;
using Atom.Domain.Extension;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Atom.Utilities;
using Atom.Controllers;
using Atom.Events.Interface;

namespace Atom
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables()
                 .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/UserSettings/AccountInfo", "/AccountSettings");
                options.Conventions.AddPageRoute("/UserSettings/ThemeSettings", "/ThemeSettings");
                options.Conventions.AddPageRoute("/EventSearch/SearchPreferences", "/SearchPreferences");

            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            });

            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddDistributedMemoryCache();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "AtomCookie";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.SlidingExpiration = true;
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(600);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<DatabaseConfiguration>(Configuration.GetSection(DatabaseConfiguration.DatabaseSection));

            services.AddOptions();

            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddHttpClient();

            AddSingletons(services);

            AddPolicies(services);
        }

        //  register policies at app startup
        private void AddPolicies(IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                //options.AddPolicy(Policies.ViewPubEvent.Description(),
                //policy => policy.RequireRole($"{Roles.Anonymous.Description()}," +
                //    $"{ Roles.Standard.Description()}," +
                //    $"{ Roles.OrgAdmin.Description()}," +
                //    $"{ Roles.OrgUsr.Description()}," +
                //    $"{ Roles.OrgCoord.Description()}," +
                //    $"{ Roles.OrgAdmin.Description()},"));

                options.AddPolicy(Policies.ViewOrgEvent.Description(),
                policy => policy.RequireRole($"{Roles.OrgAdmin.Description()}," +
                    $"{ Roles.OrgCoord.Description()}," +
                    $"{ Roles.OrgUsr.Description()},"));

                //options.AddPolicy(Policies.SearchEvent.Description(),
                //    policy => policy.RequireRole($"{Roles.Anonymous.Description()}," +
                //    $"{ Roles.Standard.Description()}," +
                //    $"{ Roles.OrgAdmin.Description()}," +
                //    $"{ Roles.OrgCoord.Description()}," +
                //    $"{ Roles.OrgUsr.Description()},"));

                options.AddPolicy(Policies.RSVP.Description(),
                     policy => policy.RequireRole($"{Roles.Standard.Description()}," +
                    $"{ Roles.OrgAdmin.Description()}," +
                    $"{ Roles.OrgCoord.Description()}," +
                    $"{ Roles.OrgUsr.Description()},"));

                options.AddPolicy(Policies.CrudEvent.Description(),
                    policy => policy.RequireRole($"{Roles.Standard.Description()}," +
                    $"{ Roles.OrgAdmin.Description()}," +
                    $"{ Roles.OrgCoord.Description()}," +
                    $"{ Roles.OrgUsr.Description()},"));

                options.AddPolicy(Policies.CrudOrgUsr.Description(),
                    policy => policy.RequireRole($"{Roles.OrgAdmin.Description()},"));

                options.AddPolicy(Policies.InviteOrg.Description(),
                policy => policy.RequireRole($"{Roles.OrgAdmin.Description()},"));


                options.AddPolicy(Policies.AcceptOrgEvent.Description(),
                policy => policy.RequireRole($"{Roles.OrgAdmin.Description()}, " +
                $"{ Roles.OrgCoord.Description()}"));

                options.AddPolicy(Policies.CapEvent.Description(),
                 policy => policy.RequireRole($"{Roles.OrgCoord.Description()}"));
            });
        }

        private void AddSingletons(IServiceCollection services)
        {
            services.AddSingleton<ISecurityManager, SecurityManager>();
            services.AddSingleton<ISecurityUtilities, SecurityUtilities>();
            services.AddSingleton<ICSSManager, CSSManager>();
            services.AddSingleton<IEventController, EventBriteController>();
            services.AddSingleton<IEventController, TicketmasterController>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

    }
}
