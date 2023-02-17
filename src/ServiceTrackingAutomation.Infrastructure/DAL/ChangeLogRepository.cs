using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ChangeLogRepository : GenericRepository<ChangeLog,BusinessDbContext>
{
    public ChangeLogRepository(BusinessDbContext context) : base(context)
    {
    }
}