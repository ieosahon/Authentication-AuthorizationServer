using AuthAuthorizationServer.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAuthorizationServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // register the identity server 4
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(MemoryConfig.IdentityResources())
                .AddInMemoryApiResources(MemoryConfig.ApiResources())
                .AddInMemoryClients(MemoryConfig.Clients())
                .AddTestUsers(MemoryConfig.TestUsers())
                .AddInMemoryApiScopes(MemoryConfig.ApiScopes())
                .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // add Identity server4 middle ware in the middle ware pipeline
            app.UseIdentityServer();
        }
    }
}
