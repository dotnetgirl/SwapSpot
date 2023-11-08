using SwapSpot.Service.Mappers;
using SwapSpot.Service.Helpers;
using SwapSpot.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Diagnostics;
using SwapSpot.Api.Extentions;
using SwapSpot.Api.Models;
using SwapSpot.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SwapSpotDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AAA", builder => builder.WithOrigins("https://82.215.96.174").AllowAnyHeader().AllowAnyMethod());
});

//JWT
builder.Services.AddJwtService(builder.Configuration);
//Swagger
builder.Services.AddSwaggerService();


builder.Services.AddCustomServices();
builder.Services.AddAutoMapper(typeof(MapperProfile));

//Configure api url name
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(
                                        new ConfigurationApiUrlName()));
});

var app = builder.Build();

// Getting full path of wwwroot
WebHostEnviromentHelper.WebRootPath = Path.GetFullPath("wwwroot");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleWare>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AAA");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
