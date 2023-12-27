using BookShop.Api.Extension;
using BookShop.Application.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddAuthenticationJwt(builder.Configuration);
builder.Services.AddSwagger();

builder.Services.InitializationRepository();
builder.Services.InitializationServices();

builder.Services.AddCors(option =>
{
    option.AddPolicy("BlazorApp", configure =>
    {
        configure.WithOrigins("https://localhost:7268")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();


app.UseCors("BlazorApp");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();