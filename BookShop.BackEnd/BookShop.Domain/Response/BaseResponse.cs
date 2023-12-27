using System.Net;

namespace BookShop.Domain.Response;

public record BaseResponse<T>(T Model, HttpStatusCode StatusCode, string Message);