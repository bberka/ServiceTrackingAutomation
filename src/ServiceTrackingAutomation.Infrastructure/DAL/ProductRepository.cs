﻿using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ProductRepository : GenericRepository<Product,BusinessDbContext>
{
    public ProductRepository(BusinessDbContext context) : base(context)
    {
    }
}