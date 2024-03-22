using Microsoft.EntityFrameworkCore;
using Pos_System.Repository.Implement;
using Pos_System.Repository.Interfaces;
using SignalRAssignment.Domain.Data;
using SignalRAssignment.Services.Implements;
using SignalRAssignment.Services.Interfaces;

public static class DependencyServices
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<ShoppingDbContext>, UnitOfWork<ShoppingDbContext>>();
        return services;
    }

    private static string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:ShoppingDB"];

        return strConn;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ShoppingDbContext>(options => options.UseSqlServer(GetConnectionString()));
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}