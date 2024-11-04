using PatikaWeek14PracticeDataProtectionAndJwt.Context;
using PatikaWeek14PracticeDataProtectionAndJwt.DataProtection;
using PatikaWeek14PracticeDataProtectionAndJwt.Dtos;
using PatikaWeek14PracticeDataProtectionAndJwt.Entities;
using PatikaWeek14PracticeDataProtectionAndJwt.Enums;
using PatikaWeek14PracticeDataProtectionAndJwt.Services;
using PatikaWeek14PracticeDataProtectionAndJwt.Types;

namespace PatikaWeek14PracticeDataProtectionAndJwt.Managers
{
    public class UserManager : IUserService
    {
        private readonly DataProtectionAndJwtDbContext _db;
        private readonly IDataProtection _dataProtector;

        public UserManager(DataProtectionAndJwtDbContext db, IDataProtection dataProtector)
        {
            _db = db;
            _dataProtector = dataProtector;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var userEntity = new UserEntity
            {
                Email = user.Email,
                Password = _dataProtector.Protect(user.Password), // Data-protection
                UserType = UserType.User
            };

            _db.Users.Add(userEntity);
            _db.SaveChanges();

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Kayıt başarılı."
            };
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _db.Users.Where(x =>  x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı."
                };
            }

            var unprotectedPassword = _dataProtector.UnProtect(userEntity.Password);

            if(unprotectedPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        UserType = userEntity.UserType,
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı."
                };
            }
        }
    }
}
