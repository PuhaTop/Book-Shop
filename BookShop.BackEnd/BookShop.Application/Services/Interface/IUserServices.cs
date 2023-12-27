using BookShop.Application.Dto;
using BookShop.Domain.Entity;
using BookShop.Domain.Response;

namespace BookShop.Application.Services.Interface;

public interface IUserServices
{
    public Task<BaseResponse<string>> Login(LoginDto dto);
    
}