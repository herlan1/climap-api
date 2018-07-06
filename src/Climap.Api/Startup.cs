using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Climap.Comum;
using Climap.Dominio.Migracoes;
using Climap.Infra.Migracoes;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Climap.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        private IClimapMigrationRunner _migrationRunner;

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddTransient<IClimapMigrationRunner, ClimapMigrationRunner>();
            //services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            //services.AddTransient<IValidator<Cliente>, ValidadorDeCliente>();
            //services.AddTransient<ServicoDeCliente, ServicoDeCliente>();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, IClimapMigrationRunner migrationRunner)
        {

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();

            _migrationRunner = migrationRunner;

            _migrationRunner.MigrateUpAll();

            Runtime.ConnectionString = Configuration.GetConnectionString("connection");
        }
    }
}
