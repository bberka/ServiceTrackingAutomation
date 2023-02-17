using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ComplaintRepository : GenericRepository<Complaint,BusinessDbContext>
{
    public ComplaintRepository(BusinessDbContext context) : base(context)
    {
    }
}