namespace Trellix.DataAccess.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trellix.DataAccess;

public static class DatabaseInstaller
{
    public static void AddDBConfiguration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<TrellixDBContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.MigrationsAssembly("Trellix.DataAccess");
                builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "MigrationHistory");
            });
        });
    }
}
