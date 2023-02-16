using EasMe;
using EasMe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Infrastructure;

namespace ServiceTrackingAutomation.Application.Manager;

public class UserManager : IUserManager
{
    private readonly BusinessDbContext _dbContext;

    public UserManager(BusinessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ResultData<User> GetUser(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
        if (user is null)
        {
            return Result.Warn(1, "Kullanıcı bulunamadı");
        }
        if (!user.IsValid)
        {
            return Result.Warn(2, "Kullanıcı geçersiz");
        }
        return user;
    }

    public ResultData<User> GetUserByEmail(string email)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.EmailAddress == email);
        if (user is null)
        {
            return Result.Warn(1, "Kullanıcı bulunamadı");
        }
        if (!user.IsValid)
        {
            return Result.Warn(2, "Kullanıcı geçersiz");
        }
        return user;
    }

    public Result UpdateUser(UserDto dto)
    {
        var userResult = GetUser(dto.Id);
        if (userResult.IsFailure)
        {
            return userResult.ToResult(100);
        }
        var user = userResult.Data;
        user.EmailAddress = dto.EmailAddress;
        user.Role = dto.Role;
        _dbContext.Update(user);
        return _dbContext.SaveChangesResult(1);
    }

    public Result DeleteUser(int id)
    {
        var userResult = GetUser(id);
        if (userResult.IsFailure)
        {
            return userResult.ToResult(100);
        }
        var user = userResult.Data;
        _dbContext.Remove(user);
        return _dbContext.SaveChangesResult(1);
    }

    public Result DisableUser(int id)
    {
        var userResult = GetUser(id);
        if (userResult.IsFailure)
        {
            return userResult.ToResult(100);
        }
        var user = userResult.Data;
        user.IsValid = false;
        _dbContext.Update(user);
        return _dbContext.SaveChangesResult(1);
    }

    public Result EnableUser(int id)
    {
        var user = _dbContext.Users.Find(id);
        if (user is null)
        {
            return Result.Warn(1, "Kullanıcı bulunamadı");
        }

        if (user.IsValid)
        {
            return Result.Warn(2, "Kullanıcı zaten geçerli");
        }
        user.IsValid = true;
        _dbContext.Update(user);
        return _dbContext.SaveChangesResult(3);
    }
}