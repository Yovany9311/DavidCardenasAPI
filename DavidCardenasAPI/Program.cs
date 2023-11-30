using DavidCardenasAPI.Business.Interfaces;
using DavidCardenasAPI.Business.Services;
using DavidCardenasAPI.Data.Context;
using DavidCardenasAPI.Data.Interfaces;
using DavidCardenasAPI.Domain.Models;
using DavidCardenasAPI.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WSServicioPichincha.Business.Services;
using WSServicioPichincha.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
           .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(builder => builder.AddSerilog());
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings").GetSection("Key").Value.ToString())),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});
builder.Services.AddDbContext<HalterofiliaContext>(options =>
{
    options.UseSqlServer(
    builder.Configuration.GetConnectionString("HalterofiliaContext"));
});


builder.Services.AddTransient<IAuthenticationJwtServices, AuthenticationJwtServices>();

builder.Services.AddTransient<ILogService, LogService>();
builder.Services.AddTransient<IDeportistasService, DeportistasService>();
builder.Services.AddTransient<IResultadoService, ResultadoService>();

builder.Services.AddTransient<IRepository<LogApi>, Repository<LogApi>>();
builder.Services.AddTransient<IRepository<Deportista>, Repository<Deportista>>();
builder.Services.AddTransient<IRepository<Resultado>, Repository<Resultado>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
