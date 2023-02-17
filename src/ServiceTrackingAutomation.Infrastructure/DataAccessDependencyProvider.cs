using EasMe.EntityFrameworkCore.V2;
using Microsoft.Extensions.DependencyInjection;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Infrastructure.DAL;

namespace ServiceTrackingAutomation.Infrastructure;

public static class DataAccessDependencyProvider
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        //services.AddDbContext<BusinessDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}