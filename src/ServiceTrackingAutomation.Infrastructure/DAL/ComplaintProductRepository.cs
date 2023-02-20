
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ComplaintProductRepository : GenericRepository<ComplaintProduct,BusinessDbContext>
{
    public ComplaintProductRepository(BusinessDbContext context) : base(context)
    {
    }
}