using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Twilio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Twilio
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddDbContext<TwilioContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var context = app.ApplicationServices.GetService<TwilioContext>();

            AddTestData(context);

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
            app.Run(async (error) =>
            {
                await error.Response.WriteAsync("Hello World!");
            });
        }

        private static void AddTestData(TwilioContext context)
        {
            context.Database.ExecuteSqlCommand("Delete From Contacts");

            var contact1 = new Contact();
            contact1.FirstName = "Rachel";
            contact1.LastName = "Smith";
            contact1.PhoneNumber = "+17144586535";

            var contact2 = new Contact();
            contact2.FirstName = "Jimmy";
            contact2.LastName = "Johns";
            contact2.PhoneNumber = "+17144586535";

            var contact3 = new Contact();
            contact3.FirstName = "Ronald";
            contact3.LastName = "McDonald";
            contact3.PhoneNumber = "+17144586535";

            context.Contacts.Add(contact1);
            context.Contacts.Add(contact2);
            context.Contacts.Add(contact3);
            context.SaveChanges();
        }
    }
}
