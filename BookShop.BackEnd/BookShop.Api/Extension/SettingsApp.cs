using System.Reflection;
using System.Security.Claims;
using System.Text;
using BookShop.Application.Repository.Implementation;
using BookShop.Application.Repository.Interface;
using BookShop.Application.Services.Implementation;
using BookShop.Application.Services.Interface;
using BookShop.Domain.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BookShop.Api.Extension;

public static class SettingsApp
{
    public static void AddAuthenticationJwt(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddAuthorization();

        service.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


        }).AddJwtBearer(option =>
        {

            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RoleClaimType = ClaimTypes.Role,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issue"]
            };
        });


    }

    public static void AddSwagger(this IServiceCollection services)
    {
        
        
        
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1",new OpenApiInfo()
            {
                Contact = new OpenApiContact()
                {
                    Name = "GitHub",
                    Url = new Uri("https://github.com/PuhaTop/Book-Shop")
                },
                Description = "Api for a book shop. Using net 6 technologies📖",
                Title = "Book Shop Api",
                Version = "v1",
            });    
            
            option.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Please enter your token",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            
            
            option.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });


            var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,xml));

        });
    }

    public static void InitializationRepository(this IServiceCollection collection)
    {
        collection.AddScoped<IBaseRepository<User>,UserRepository>();
    }


    public static void InitializationServices(this IServiceCollection collection)
    {
        collection.AddScoped<IUserServices, UserServices>();
    }
    
}