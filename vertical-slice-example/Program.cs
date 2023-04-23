using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DataContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        await dbContext.Database.MigrateAsync();
        await Seeder.SeedAsync(dbContext);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occured during migration or seeding");
    }
}

app.Run();