using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SwapSpot.Domain.Entities.Users;
using SwapSpot.Service.DTOs.Users.Logins;
using SwapSpot.Service.Exceptions;
using SwapSpot.Service.Interfaces.Authorizations;
using SwapSpot.Service.Interfaces.Users;
using SwapSpot.Shared.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwapSpot.Service.Services.Users;

public class LoginService : ILoginService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IRoleService _roleService;

    public LoginService(IUserService userService, IConfiguration configuration, IRoleService roleService)
    {
        _userService = userService;
        _configuration = configuration;
        _roleService = roleService;
    }

    public async Task<UserLoginResultDto> LoginAsync(UserForLoginDto userForLoginDto)
    {
        var user = await _userService.RetrieveByEmailAsync(userForLoginDto.Email);
        if (user is null || !PasswordHelper.Verify(userForLoginDto.Password, user.Password))
            throw new SwapSpotException(400, "Email or password is incorrect");

        var role = await _roleService.RetrieveByIdForLoginAsync(user.RoleId);
        user.Role = role;
        return new UserLoginResultDto
        {
            Token = GenerateToken(user)
        };
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
                 new Claim(ClaimTypes.Name, user.FirstName)
            }),
            Audience = _configuration["JWT:Audience"],
            Issuer = _configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
