using KymetaHub.sdk;
using KymetaHub.sdk.Application;
using KymetaHub.sdk.Extensions;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("kymetahub.test")]

//args = args
//    .SelectMany(x => x.Split(';'))
//    .ToArray();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging
    .AddConsole()
    .AddFilter(x => true);

ApplicationOption option = builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddCommandLine(args)
    .AddUserSecrets("KymetaHubApi")
    .AddEnvironmentVariables()
    .Build()
    .Bind<ApplicationOption>()
    .Verify();

builder.Services.AddSingleton(option);
builder.Services.ConfigureKymeta();

//builder.Services.AddCors(x => x.AddPolicy("default", builder =>
//{
//    builder
//        .AllowAnyOrigin()
//        .AllowAnyMethod()
//        .AllowAnyHeader()
//        .SetPreflightMaxAge(TimeSpan.FromHours(1));
//}));

//  ///////////////////////////////////////////////////////////////////////////
var app = builder.Build();

app.Logger.LogInformation("ApplicationOption={option}", option.ToString());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("default");
app.UseAuthorization();
app.MapControllers();

app.Run();
