using PatikaWeek14PracticeDataProtectionAndJwt.Dtos;
using PatikaWeek14PracticeDataProtectionAndJwt.Types;

namespace PatikaWeek14PracticeDataProtectionAndJwt.Services
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}
