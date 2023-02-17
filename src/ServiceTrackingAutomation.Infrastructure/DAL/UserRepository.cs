﻿using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

public class UserRepository : GenericRepository<User,BusinessDbContext>
{
    public UserRepository(BusinessDbContext context) : base(context)
    {

    }
}