using JobsApi.Configurations;
using JobsApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Registration of Config class
            services.AddSingleton<IConfig>(Configuration.GetSection("CustomConfig")?.Get<Config>());

            services.AddControllers();

            //Registration of AddDbContexts method
            AddDbContexts(services);
        }

        public void AddDbContexts(IServiceCollection services)
        {
            var debugLogging = new Action<DbContextOptionsBuilder>(opt =>
            {
#if DEBUG
                // this will log EF generated SQL commands to the console
                opt.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                }));

                // This will log the params for those commands to the console
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
#endif
            });
            
            // Actual DbContext registration
            services.AddDbContext<JobsDbContext>(options =>
            {
                var conString = Configuration.GetConnectionString("sqldb-job")?? "name= JobDb";
                options.UseSqlServer(conString, opt => opt.EnableRetryOnFailure(5));
                debugLogging(options);
            }, ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
