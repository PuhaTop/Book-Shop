using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookShop.Application.Dto;
using BookShop.Application.Repository.Interface;
using BookShop.Application.Services.Interface;
using BookShop.Domain.Entity;
using BookShop.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Application.Services.Implementation;

public class UserServices : IUserServices
{

    private readonly IBaseRepository<User> _baseRepository;
    private readonly IConfiguration _configuration;

    public UserServices(IBaseRepository<User> baseRepository,IConfiguration configuration)
    {
        _baseRepository = baseRepository;
        _configuration = configuration;
    }
    
    public async Task<BaseResponse<string>> Login(LoginDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return new BaseResponse<string>("Check Data",HttpStatusCode.UnprocessableEntity,"Please check your data");
            }

            var user = await _baseRepository.Get().FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (user is null)
            {
                return new BaseResponse<string>("User not found",HttpStatusCode.NotFound,$"We did not find a user with the login {dto.Login}");
            }

            if (!VerifyPassword(dto.Password,user.PasswordHash,user.PasswordSalt))
            {
                return new BaseResponse<string>("Check your password", HttpStatusCode.Unauthorized,
                    "Error password is not valid");
            }

            var jwt = CreateToken(user);

            return new BaseResponse<string>(jwt,HttpStatusCode.OK,"jwt creation completed successfully");

        }
        catch (Exception e)
        {
            return new BaseResponse<string>("Exception", HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,user.Login)
            
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

        var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

        var jwt = new JwtSecurityToken(
                claims:claims,
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issue"],
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    
}


