

namespace ServiceTrackingAutomation.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();
    public UnitOfWork()
    {
        _dbContext = new();
    }

    private readonly BusinessDbContext _dbContext;

    public bool IsDisposed { get; private set; } = false;


    #region REPOSITORIES
    private IGenericRepository<ChangeLog>? _changeLogRepository;
    public IGenericRepository<ChangeLog> ChangeLogRepository
    {
        get
        {
            _changeLogRepository ??= new ChangeLogRepository(_dbContext);
            return _changeLogRepository;
        }
    }

    private IGenericRepository<Complaint>? _complaintRepository;
    public IGenericRepository<Complaint> ComplaintRepository
    {
        get
        {
            _complaintRepository ??= new ComplaintRepository(_dbContext);
            return _complaintRepository;
        }
    }

    private IGenericRepository<ComplaintProduct>? _complaintProductRepository;
    public IGenericRepository<ComplaintProduct> ComplaintProductRepository
    {
        get
        {
            _complaintProductRepository ??= new ComplaintProductRepository(_dbContext);
            return _complaintProductRepository;
        }
    }

    private IGenericRepository<Customer>? _customerRepository;

    public IGenericRepository<Customer> CustomerRepository
    {
        get
        {
            _customerRepository ??= new CustomerRepository(_dbContext);
            return _customerRepository;
        }
    }

    private IGenericRepository<CustomerContact>? _customerContactRepository;

    public IGenericRepository<CustomerContact> CustomerContactRepository
    {
        get
        {
            _customerContactRepository ??= new CustomerContactRepository(_dbContext);
            return _customerContactRepository;
        }
    }

    private IGenericRepository<Product>? _productRepository;

    public IGenericRepository<Product> ProductRepository
    {
        get
        {
            _productRepository ??= new ProductRepository(_dbContext);
            return _productRepository;
        }
    }

    private IGenericRepository<ProductType>? _productTypeRepository;

    public IGenericRepository<ProductType> ProductTypeRepository
    {
        get
        {
            _productTypeRepository ??= new ProductTypeRepository(_dbContext);
            return _productTypeRepository;
        }
    }

    private IGenericRepository<Service>? _serviceRepository;

    public IGenericRepository<Service> ServiceRepository
    {
        get
        {

            _serviceRepository ??= new ServiceRepository(_dbContext);
            return _serviceRepository;

        }
    }


    private IGenericRepository<ServiceAction>? _serviceActionRepository;

    public IGenericRepository<ServiceAction> ServiceActionRepository
    {
        get
        {
            _serviceActionRepository ??= new ServiceActionRepository(_dbContext);
            return _serviceActionRepository;
        }
    }

    private IGenericRepository<User>? _userRepository;

    public IGenericRepository<User> UserRepository
    {
        get
        {
            _userRepository ??= new UserRepository(_dbContext);
            return _userRepository;

        }
    }


    #endregion

    public void Dispose()
    {
        if(IsDisposed) return;
        IsDisposed = true;
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
    public bool SaveBool()
    {
        return Save() > 0;
    }
    public Result SaveResult(ushort rv, [CallerMemberName] string operationName = "")
    {
        var dbResult = Save() > 0;
        if (dbResult) return Result.Success(operationName);
        return Result.Fatal(rv, "Kritik bir sorun oluştu, lütfen tekrar deneyiniz");
    }
    public int Save()
    {
        var entityNames = DetectChangedProperties();
        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            var affected = _dbContext.SaveChanges();
            if (affected > 0)
            {
                transaction.Commit();
                return affected;
            }
        }
        catch (Exception ex)
        {
            if (!entityNames.IsNullOrEmpty())
            {
                logger.Fatal(ex, "InternalDbError", entityNames);
            }
            else
            {
                logger.Fatal(ex, "InternalDbError");
            }
        }
        transaction.Rollback();
        return 0;
    }

    /// <summary>
    /// Detects changed entities and properties and inserts it to <see cref="ChangeLogRepository"/> before saving database
    /// </summary>
    /// <returns>Changed entity names</returns>
    private string DetectChangedProperties()
    {
        var modifiedEntities = _dbContext.ChangeTracker
            .Entries()
            .Where(p => p.State == EntityState.Modified)
            .ToList();

        var entityNames = "";
        var logs = new List<ChangeLog>();
        foreach (var change in modifiedEntities)
        {
            var entityName = change.Entity.GetType().Name;
            entityNames += entityName + ",";
            foreach (var prop in change.OriginalValues.Properties)
            {
                var originalValue = change.OriginalValues[prop]?.ToString();
                var currentValue = change.CurrentValues[prop]?.ToString();
                if (originalValue == currentValue) continue;
                ChangeLog log = new ChangeLog()
                {
                    EntityName = entityName,
                    PropertyName = prop.Name,
                    OldValue = originalValue,
                    NewValue = currentValue,
                };
                logs.Add(log);
            }
        }
        ChangeLogRepository.InsertRange(logs);
        return entityNames;
    }
}