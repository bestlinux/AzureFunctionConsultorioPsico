using FunctionConsultorio.Application.Services;
using FunctionConsultorio.Domain.Entities;
using FunctionConsultorio.Infra.Services;
using FunctionConsultorio.Persistence.Context;
using FunctionConsultorio.Persistence.Services;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureEmail(builder.Configuration);
builder.Services.ConfigureApplicationApp();


builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
options =>
{
    /*options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Issuer"],
        ValidAudience = builder.Configuration["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Key"]))
    };*/
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY")!))
    };
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


var app = builder.Build();

app.Run();

