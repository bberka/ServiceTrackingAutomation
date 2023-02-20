
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ChangeLogRepository : GenericRepository<ChangeLog,BusinessDbContext>
{
    public ChangeLogRepository(BusinessDbContext context) : base(context)
    {
    }
}