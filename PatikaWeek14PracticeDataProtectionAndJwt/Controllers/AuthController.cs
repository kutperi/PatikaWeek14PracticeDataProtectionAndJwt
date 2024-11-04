using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatikaWeek14PracticeDataProtectionAndJwt.Dtos;
using PatikaWeek14PracticeDataProtectionAndJwt.Jwt;
using PatikaWeek14PracticeDataProtectionAndJwt.Models;
using PatikaWeek14PracticeDataProtectionAndJwt.Services;

namespace PatikaWeek14PracticeDataProtectionAndJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.AddUser(addUserDto);

            if(result.IsSucceed)
                return Ok(result.Message);
            else
                return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.LoginUser(new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password,
            });

            if(!result.IsSucceed)
                return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)

            });

            return Ok(new LoginResponse
            {
                Message = "Giriş başarıyla tamamlandı.",
                Token = token
            });
        }
    }
}
