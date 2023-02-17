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
    internal class BusinessDbContext : DbContext
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