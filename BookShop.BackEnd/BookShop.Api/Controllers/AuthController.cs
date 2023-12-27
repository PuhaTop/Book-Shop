using System.Net;
using BookShop.Application.Dto;
using BookShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace BookShop.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{

    private readonly IUserServices _userServices;

    public AuthController(IUserServices userServices)
    {
        _userServices = userServices;
    }
    
    /// <summary>
    ///  method for authorization
    /// </summary>
    /// <param name="loginDto">login model</param>
    /// <response code= "200"> Method completed successfully </response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        var result = await _userServices.Login(loginDto);
        
        
        
        return result.StatusCode switch
        {
            HttpStatusCode.OK => Ok(new
            {
                result.StatusCode,
                Token = result.Model
            }),
            _ => StatusCode((int)result.StatusCode,new
            {
               result.StatusCode,
               result.Message
            })
        };
    }
}