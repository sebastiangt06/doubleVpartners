using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DoubleV.Infrastructure.Database;
using DoubleV.Infrastructure.Repositories.Persons;
using DoubleV.Domain.Services.Interfaces.Persons;
using DoubleV.Domain.Services.Interfaces.Users;
using DoubleV.Infrastructure.Security;
using DoubleV.Application.Common.Interfaces;
using DoubleV.Infrastructure.Repositories;
using DoubleV.Application.Interfaces;

namespace DoubleV.Infrastructure
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLConnectionStrings");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'SQLConnectionStrings' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));


            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();


            return services;
        }
    }
}
