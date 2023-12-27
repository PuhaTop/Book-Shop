using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BookShop.Admin.Providers;

public class CustomAuthProvider : AuthenticationStateProvider
{

    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _client;

    public CustomAuthProvider(ILocalStorageService localStorage , HttpClient client)
    {
        _localStorage = localStorage;
        _client = client;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        var token = await _localStorage.GetItemAsStringAsync("token");

        var claimIdentity = new ClaimsIdentity();

        if (!string.IsNullOrEmpty(token))
        {
            claimIdentity = new(Decoder(token), "jwt");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                token.Replace("\"",""));
        }

        var claimPrincipal = new ClaimsPrincipal(claimIdentity);

        var authState = new AuthenticationState(claimPrincipal);
        
        NotifyAuthenticationStateChanged(Task.FromResult(authState));


        return authState;

    }

    private IEnumerable<Claim> Decoder(string jwt)
    {
        var token = jwt.Split('.')[1];
        var base64 = ParseBase64WithoutPadding(token);
        var keyValue = JsonSerializer.Deserialize<Dictionary<string, object>>(base64);
        return keyValue!.Select(x => new Claim(x.Key, x.Value.ToString()));

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