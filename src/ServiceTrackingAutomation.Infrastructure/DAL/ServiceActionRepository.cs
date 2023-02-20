
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ServiceActionRepository : GenericRepository<ServiceAction,BusinessDbContext>
{
    public ServiceActionRepository(BusinessDbContext context) : base(context)
    {
    }
}