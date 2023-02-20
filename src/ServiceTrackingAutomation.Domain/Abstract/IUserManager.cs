

namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IUserManager
{
    ResultData<User> GetUser(int id);
    ResultData<User> GetUserByEmail(string email);
    Result UpdateUser(UserDto dto);
    Result DeleteUser(int id);
    Result DisableUser(int id);
    Result EnableUser(int id);


}