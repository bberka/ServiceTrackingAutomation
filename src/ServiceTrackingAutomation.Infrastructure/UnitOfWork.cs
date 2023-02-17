using System.Runtime.CompilerServices;
using EasMe.EntityFrameworkCore.V2;
using EasMe.Extensions;
using EasMe.Logging;
using EasMe.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Infrastructure.DAL;

namespace ServiceTrackingAutomation.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private static readonly IEasLog logger = EasLogFactory.CreateLogger();
    public UnitOfWork()
    {
        dbContext = new BusinessDbContext();
        ChangeLogRepository = new ChangeLogRepository(dbContext);
        ComplaintRepository = new ComplaintRepository(dbContext);
        ComplaintProductRepository = new ComplaintProductRepository(dbContext);
        CustomerRepository = new CustomerRepository(dbContext);
        CustomerContactRepository = new CustomerContactRepository(dbContext);
        ProductRepository = new ProductRepository(dbContext);
        ProductTypeRepository = new ProductTypeRepository(dbContext);
        ServiceRepository = new ServiceRepository(dbContext);
        ServiceActionRepository = new ServiceActionRepository(dbContext);
        UserRepository = new UserRepository(dbContext);
    }

    private readonly BusinessDbContext dbContext;
    public IGenericRepository<ChangeLog> ChangeLogRepository { get; }
    public IGenericRepository<Complaint> ComplaintRepository { get; }
    public IGenericRepository<ComplaintProduct> ComplaintProductRepository { get; }
    public IGenericRepository<Customer> CustomerRepository { get; }
    public IGenericRepository<CustomerContact> CustomerContactRepository { get; }
    public IGenericRepository<Product> ProductRepository { get; }
    public IGenericRepository<ProductType> ProductTypeRepository { get; }
    public IGenericRepository<Service> ServiceRepository { get; }
    public IGenericRepository<ServiceAction> ServiceActionRepository { get; }
    public IGenericRepository<User> UserRepository { get; }
    
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
        using var transaction = dbContext.Database.BeginTransaction();
        try
        {
            var affected = dbContext.SaveChanges();
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
        var modifiedEntities = dbContext.ChangeTracker
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