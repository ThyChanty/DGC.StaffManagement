using DGC.Staff_Management.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DGC.StaffManagement.Infrastructure.ServiceCollectionExtensions
{

    public static class PersistenceStorageServiceCollectionExtensions
    {
        public static IServiceCollection RegisterPersistenceStorage(this IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddSingleton<IDefaultDbContextFactory, DefaultDbContextFactory>();
            services.AddScoped(provider =>
            {
                var factory = provider.GetRequiredService<IDefaultDbContextFactory>();
                return factory.CreateContext();
            });
            services.AddUnitOfWork<ApplicationDbContext>();

            return services;
        }
    }
}
