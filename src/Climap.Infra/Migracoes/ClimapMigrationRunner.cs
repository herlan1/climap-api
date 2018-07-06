using System;
using System.Reflection;
using Climap.Comum;
using Climap.Dominio.Migracoes;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Climap.Infra.Migracoes
{
    public class ClimapMigrationRunner : IClimapMigrationRunner
    {
        public void MigrateUpAll()
        {

            var assembly = Assembly.GetAssembly(typeof(ClimapMigrationRunner));

            var serviceProvider = new ServiceCollection()
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddMySql5()
                        .WithGlobalConnectionString(Runtime.ConnectionString)
                        .WithMigrationsIn(assembly))
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {

                try
                {
                    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                    runner.MigrateUp();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }
    }
}
