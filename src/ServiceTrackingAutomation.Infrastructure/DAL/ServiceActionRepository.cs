using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

public class ServiceActionRepository : GenericRepository<ServiceAction,BusinessDbContext>
{
    public ServiceActionRepository(BusinessDbContext context) : base(context)
    {
    }
}