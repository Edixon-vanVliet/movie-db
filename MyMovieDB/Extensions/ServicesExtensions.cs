using Microsoft.EntityFrameworkCore;
using MyMovieDB.Data;
using MyMovieDB.Models;

namespace MyMovieDB.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IRepository<Movie>, MovieRepository>();
    }
}