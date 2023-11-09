using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SwapSpot.DAL.IRepositories;
using SwapSpot.DAL.IRepositories.Authorizations;
using SwapSpot.DAL.Repositories;
using SwapSpot.DAL.Repositories.Authorizations;
using SwapSpot.Service.Interfaces.Addresses;
using SwapSpot.Service.Interfaces.Assets;
using SwapSpot.Service.Interfaces.Authorizations;
using SwapSpot.Service.Interfaces.Users;
using SwapSpot.Service.Services.Addresses;
using SwapSpot.Service.Services.Authorizations;
using SwapSpot.Service.Services.UserAssets;
using SwapSpot.Service.Services.Users;
using System.Reflection;
using System.Text;

namespace SwapSpot.Api.Extentions;

public static class ServiceExtentions 
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        //Users
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();

        //Assets
        services.AddScoped<IUserAssetRepository, UserAssetRepository>();
        services.AddScoped<IUserAssetService, UserAssetService>();
        services.AddScoped<IUserAssetPhotoRepository, UserAssetPhotoRepository>();

        //Messages
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageService, MessageService>();

        //Addresses
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IAddressService, AddressService > ();

        //Roles
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRoleService, RoleService>();

        //Permissions
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPermissionService, PermissionService>();

        //Role-Permission
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();

        //Logins
        services.AddScoped<ILoginService, LoginService>();

    }
    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shamsheer.Api", Version = "v1" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }});
        });
    }
}