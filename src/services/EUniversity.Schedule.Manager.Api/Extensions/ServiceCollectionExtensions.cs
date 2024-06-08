using System.Reflection;
using EUniversity.Schedule.Manager.Data;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Schedule.Manager.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Add EF database context
        services.AddDbContextFactory<UniversityScheduleManagerContext>(
           options => options.UseSqlServer(
               configuration.GetConnectionString("ScheduleManager"),
               b => b.MigrationsAssembly("EUniversity.Schedule.Manager.Data")));

        return services;
    }
}
