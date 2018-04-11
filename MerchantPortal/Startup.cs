using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MerchantPortal.Data;
using MerchantPortal.Data.Models;
using MerchantPortal.Services;
using MerchantPortal.Areas.MasterSetup.Data;
using MerchantPortal.Data.Concrete;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Logging;
using System.IO;
using AutoMapper;
using MerchantPortal.Models;
using MerchantPortal.Helper;

namespace MerchantPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            Configuration = configuration;

            var logfile = Path.Combine(env.ContentRootPath, "ContentFile.txt");
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel
                            .Override("System", LogEventLevel.Debug)
                            .WriteTo.File(logfile)
                            //.WriteTo.File(@"C:\users\jeremy\Desktop\log.txt")
                            //.RollingFile("log-{Date}.txt", LogEventLevel.Error)                           
                            .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            /*
             services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
            */
            services.AddDbContext<MerchantPortalDBContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<MerchantPortalDBContext>()
                .AddDefaultTokenProviders();

            /*Model Mapping */
            MapperInitialize.ModelMap();
            /*------------------ Start Serilog ----------------*/
            //services.AddSingleton<Serilog.ILogger>();
            services.AddLogging();
            /*------------------ End Serilog ---------------- */


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //----------- Adding Serilog for logging------------
            loggerFactory.AddSerilog();
            //----------- Adding Microsoft Logger for logging------------

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

            }

            app.UseStaticFiles();
            app.UseAuthentication();


            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}"
                    //defaults: "{controller=Account}/{action=Login}/{id?}"
                   );


            });
        }
    }
}
