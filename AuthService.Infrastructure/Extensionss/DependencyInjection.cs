using AuthService.Application.Features.Interfaces;
using AuthService.Application.Services;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Repositories;
using AuthService.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Application.Services;
using FluentValidation;
using AuthService.Infrastructure.Authentication;

namespace AuthService.Infrastructure.Extensionss
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService.Application.Services.AuthService>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
