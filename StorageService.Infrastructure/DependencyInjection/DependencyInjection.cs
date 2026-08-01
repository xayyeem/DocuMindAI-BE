using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorageService.Application.Features.Interfaces;
using StorageService.Application.Services;
using StorageService.Infrastructure.Persistence;
using StorageService.Infrastructure.Persistence.Configurations;
using StorageService.Infrastructure.Repositories;
using StorageService.Infrastructure.Storage;

namespace StorageService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));

            services.Configure<StorageOptions>(
                configuration.GetSection(StorageOptions.SectionName));

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IDocumentRepository, DocumentRepository>();

            services.AddScoped<IDocumentStorage, LocalDocumentStorage>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentService, DocumentService>();
            return services;
        }
    }
}