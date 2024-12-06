using HairScheduler.Domain.Repositories;
using HairScheduler.Domain.Repositories.Interfaces;
using HairScheduler.Infrastructure.Data;
using HairScheduler.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HairScheduler.Infrastructure;

public static class DependencyInjection
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWriteOnlyRepository, Repository>();
        services.AddScoped<IReadOnlyRepository, Repository>();
        services.AddScoped<IUpdateOnlyRepository, Repository>();
        services.AddScoped<IDeleteRepository, Repository>();

      


    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionDB");
        var version = new Version(8, 0, 40);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<AppDbContext>(config => config.UseMySql(connectionString, serverVersion));

    }

}
