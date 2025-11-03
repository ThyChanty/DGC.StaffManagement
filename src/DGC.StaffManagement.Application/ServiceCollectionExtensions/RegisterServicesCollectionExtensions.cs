using DGC.StaffManagement.Application.Interfaces;
using DGC.StaffManagement.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGC.StaffManagement.Application.ServiceCollectionExtensions
{
    public static class RegisterServicesCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            // Register Serice Here
            services.AddScoped<IStaffService, StaffService>();

            
            return services;
        }
    }
}
