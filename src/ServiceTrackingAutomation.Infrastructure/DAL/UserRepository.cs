
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class UserRepository : GenericRepository<User,BusinessDbContext>
{
    public UserRepository(BusinessDbContext context) : base(context)
    {

    }
}