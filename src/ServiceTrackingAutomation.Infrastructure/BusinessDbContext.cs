using System.Configuration;
using EasMe.Extensions;
using EasMe.Logging;
using EasMe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure
{
    public class BusinessDbContext : DbContext
    {
        private static readonly IEasLog logger = EasLogFactory.CreateLogger();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ServiceDb"].ConnectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
        }
      
        public bool SaveChangesBool()
        {
            return SaveChanges() > 0;
        }
        public Result SaveChangesResult(ushort rv,string operationName = "")
        {
            var dbResult = SaveChanges() > 0;
            if (dbResult) return Result.Success(operationName);
            return Result.Fatal(rv, "Kritik bir sorun oluştu, lütfen tekrar deneyiniz");
        }
        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();

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
            ChangeLogs.AddRange(logs);
            using var transaction = Database.BeginTransaction();
            try
            {
                var affected = base.SaveChanges();
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
                    logger.Fatal(ex, "InternalDbError",entityNames);
                }
                else
                {
                    logger.Fatal(ex, "InternalDbError");
                }
            }
            transaction.Rollback();
            return 0;
        }
        
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintProduct> ComplaintProducts  { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAction> ServiceActions { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<User> Users { get; set; }

    }
}