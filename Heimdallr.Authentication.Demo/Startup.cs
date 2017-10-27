using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Heimdallr.Authentication.Demo
{
    public class Startup
    {
        private readonly bool _dev;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _dev = env.IsDevelopment();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_dev)
            {
                services
                    .AddAuthentication(DevelopmentAuthenticationDefaults.AuthenticationScheme)
                    .AddDevelopment(new DevelopmentUser
                    {
                        Username = "svasorcery",
                        Password = "gfhjkm123",
                        Subject = "S-0-0-00-0000000000-0000000000-0000000000-0000",
                        Roles = new string[] { "IT", "Developer" }
                    });
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DevelopmentAccess", policy => policy.RequireRole("Developer"));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAuthentication();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages();

            app.UseMvcWithDefaultRoute();
        }
    }
}
