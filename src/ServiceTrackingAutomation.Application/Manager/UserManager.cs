

using EasMe;
using EasMe.Extensions;
using ServiceTrackingAutomation.Domain.Models;

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

    public Result UpdateUser(User user)
    {
        var userResult = GetUser(user.Id);
        if (userResult.IsFailure)
        {
            return userResult.ToResult(100);
        }
        user = userResult.Data;
        user.EmailAddress = user.EmailAddress;
        user.Role = user.Role;
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

    public List<User> GetUsers(int exceptUserId)
    {
        return _unitOfWork.UserRepository.Get(x => x.IsValid == true && x.Id != exceptUserId).ToList();
    }

    public List<User> GetInvalidUsers()
    {
        return _unitOfWork.UserRepository.Get(x => x.IsValid == false).ToList();
    }

    public Result ChangePassword(int userId, ChangePasswordModel model)
    {
        if (model.NewPassword != model.NewPasswordConfirm)
        {
            return Result.Warn(1, "Yeni şifre ve onay şifresi eşleşmiyor");

        }

        var userResult = GetUser(userId);
        if (userResult.IsFailure)
        {
            return userResult.ToResult(100);
        }
        var user = userResult.Data;
        if (user.EncryptedPassword != model.OldPassword.MD5Hash().ToBase64String())
        {
            return Result.Warn(1, "Eski şifre hatalı");
        }
        user.EncryptedPassword = model.NewPassword.MD5Hash().ToBase64String();
        _unitOfWork.UserRepository.Update(user);
        return _unitOfWork.SaveResult(1);

    }

  

    public Result CreateUser(User user)
    {
        var emailExists = _unitOfWork.UserRepository.Any(x => x.EmailAddress == user.EmailAddress);
        if (emailExists)
        {
            return Result.Warn(1, "Bu email adresi zaten kullanılıyor");
        }

        user.IsValid = true;
        user.RegisterDate = DateTime.Now;
        user.EncryptedPassword = user.EncryptedPassword.MD5Hash().ToBase64String();
        _unitOfWork.UserRepository.Insert(user);
        return _unitOfWork.SaveResult(1);
    }
}