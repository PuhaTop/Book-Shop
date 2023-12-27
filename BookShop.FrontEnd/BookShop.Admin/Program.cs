using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookShop.Admin;
using BookShop.Admin.Providers;
using DevExpress.Blazor;
using DevExpress.Blazor.Configuration;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddDevExpressBlazor();

builder.Services.Configure<GlobalOptions>(x =>
{
    x.BootstrapVersion = BootstrapVersion.v5;
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();