using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

public class ComplaintProductRepository : GenericRepository<ComplaintProduct,BusinessDbContext>
{
    public ComplaintProductRepository(BusinessDbContext context) : base(context)
    {
    }
}