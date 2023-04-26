using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Features.Authors.Commands;
using vertical_slice_example.Features.Books.Commands;
using vertical_slice_example.Middlewares;

namespace vertical_slice_example.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => { options.UseSqlite(configuration.GetConnectionString("Default")); });
        
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<ExceptionHandlingMiddleware>();
        
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        services.AddTransient<IValidator<AddAuthorCommand>, AddAuthorCommandValidator>();
        services.AddTransient<IValidator<UpdateAuthorByIdCommand>, UpdateAuthorByIdCommandValidator>();
        
        services.AddTransient<IValidator<AddBookCommand>, AddBookCommandValidator>();
        services.AddTransient<IValidator<UpdateBookByIdCommand>, UpdateBookByIdCommandValidator>();

        return services;
    }
}