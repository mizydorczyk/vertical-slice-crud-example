using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;

namespace vertical_slice_example.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Default"));
        });
        
        return services;
    }
}