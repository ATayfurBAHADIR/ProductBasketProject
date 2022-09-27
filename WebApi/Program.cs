using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using WebApi.Extensions;
using Microsoft.Extensions.Configuration;
using Domain.Services;
using Microsoft.AspNetCore.Authentication;
using WebApi.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpContextAccessor();


MDbPersistence.Configure();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddSingleton(sp => sp.ConfigureRedis(configuration));


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
                )
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
