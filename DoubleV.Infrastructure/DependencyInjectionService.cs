using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DoubleV.Infrastructure.Database;

namespace DoubleV.Infrastructure
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "";

            try
            {
                connectionString = configuration.GetConnectionString("SQLConnectionStrings") ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;

        }
    }
}