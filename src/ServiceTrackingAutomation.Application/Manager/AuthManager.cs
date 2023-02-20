using EasMe;
using EasMe.Extensions;
using EasMe.Models;
using EasMe.Result;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Domain.Models;
using ServiceTrackingAutomation.Infrastructure;

namespace ServiceTrackingAutomation.Application.Manager;

public class AuthManager : IAuthManager
{
    private readonly IUserManager _userManager;

    public AuthManager(IUserManager userManager)
    {
        _userManager = userManager;
    }
    public ResultData<User> Authenticate(LoginModel model)
    {
        var userResult = _userManager.GetUserByEmail(model.EmailAddress);
        if (userResult.IsFailure)
        {
            return userResult.ToResult(100);
        }
        var encryptedPassword = model.Password.MD5Hash().ToBase64String();
        if (string.CompareOrdinal(encryptedPassword, userResult.Data.EncryptedPassword) != 0)
        {
            return Result.Warn(1, "Şifre yanlış");
        }
        return userResult.Data;
    }
}