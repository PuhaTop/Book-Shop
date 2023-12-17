using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BookShop.Admin.Providers;

public class CustomAuthProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        var identity = new ClaimsIdentity();

        var user = new ClaimsPrincipal(identity);

        var state = new AuthenticationState(user);
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;

    }

    private IEnumerable<Claim> Decoder(string jwt)
    {
        var token = jwt.Split('.')[1];
        var base64 = ParseBase64WithoutPadding(token);
        var keyValue = JsonSerializer.Deserialize<Dictionary<string, object>>(base64);
        return keyValue.Select(x => new Claim(x.Key, x.Value.ToString()));

    }

    private byte[] ParseBase64WithoutPadding(string token)
    {
        switch (token.Length % 4)
        {
            case 2 : token += "==";
                break;
            case 3: token += "=";
                break;
        }

        return Convert.FromBase64String(token);
    }
}