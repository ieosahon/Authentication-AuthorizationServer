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

            // calling the TryRunMigrationsAndSeedDatabase method
            TryRunMigrationsAndSeedDatabase(app);
        }
        /// <summary>
        /// Migrattion of db and seeding of database
        /// </summary>
        /// <param name="app"></param>
        private void TryRunMigrationsAndSeedDatabase(IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetService<IConfig>();
            if (config?.RunDbMigrations == true) // check if config is null and RunDbMigrations is set to true
            {
                using var scope = app.ApplicationServices.CreateScope();
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<JobsDbContext>();
                    dbContext.Database.Migrate(); // to run all pending migrations and change the db. If it is called the first time , it will create the db and tables
                }
            }

            // to seed the db
            if (config?.SeedDb == true) // check if config is null and SeedDb is set to true
            {
                using var scope = app.ApplicationServices.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<JobsDbContext>();
                DatabaseInitializer.Initializer(dbContext); // to seed the  db after the creation of the db
            }

        }
    }
}
