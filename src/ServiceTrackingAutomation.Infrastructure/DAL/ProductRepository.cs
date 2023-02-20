
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ProductRepository : GenericRepository<Product,BusinessDbContext>
{
    public ProductRepository(BusinessDbContext context) : base(context)
    {
    }
}