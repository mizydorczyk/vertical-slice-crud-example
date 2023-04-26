using System.Reflection;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Middlewares;

namespace vertical_slice_example.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => { options.UseSqlite(configuration.GetConnectionString("Default")); });

        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<ExceptionHandlingMiddleware>();

        return services;
    }
}