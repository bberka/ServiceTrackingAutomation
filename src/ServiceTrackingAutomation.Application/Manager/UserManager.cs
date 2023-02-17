using EasMe.Models;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Application.Manager;

public class UserManager : IUserManager
{
    private readonly IUnitOfWork _unitOfWork;

    public UserManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public ResultData<User> GetUser(int id)
    {
        var user = _unitOfWork.UserRepository.GetFirstOrDefault(x => x.Id == id);
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
        var user = _unitOfWork.UserRepository.GetFirstOrDefault(x => x.EmailAddress == email);
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
        _unitOfWork.UserRepository.Update(user);
        return _unitOfWork.SaveResult(1);
    }

    public Result DeleteUser(int id)
    {
        var userResult = GetUser(id);
        if (userResult.IsFailure)
        {
            return userResult.ToResult(100);
        }
        var user = userResult.Data;
        _unitOfWork.UserRepository.Delete(user);
        return _unitOfWork.SaveResult(1);
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
        _unitOfWork.UserRepository.Update(user);
        return _unitOfWork.SaveResult(1);
    }

    public Result EnableUser(int id)
    {
        var user = _unitOfWork.UserRepository.GetById(id);
        if (user is null)
        {
            return Result.Warn(1, "Kullanıcı bulunamadı");
        }

        if (user.IsValid)
        {
            return Result.Warn(2, "Kullanıcı zaten geçerli");
        }
        user.IsValid = true;
        _unitOfWork.UserRepository.Update(user);
        return _unitOfWork.SaveResult(3);
    }
}