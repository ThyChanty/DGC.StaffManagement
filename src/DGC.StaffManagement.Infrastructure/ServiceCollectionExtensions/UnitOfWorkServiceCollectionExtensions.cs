
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DGC.StaffManagement.Application.Interfaces;
using DGC.StaffManagement.Infrastructure.Repositories;

namespace DGC.StaffManagement.Infrastructure.ServiceCollectionExtensions
{

    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }
    }

}
