using System.Reflection;
using Application.Invoices.Queries.Get;
using MediatR;
using Application;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Serilog;
using Presentation.Middlewares;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Formatters;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.UseSerilog();
builder.Services.AddControllers(options =>
{
    var noContentFormatter = options.OutputFormatters.OfType<HttpNoContentOutputFormatter>().FirstOrDefault();
    if (noContentFormatter != null)
    {
        noContentFormatter.TreatNullValueAsNoContent = false;
    }
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 512 * 1024 * 1024;
});

//Configuracion CORS
builder.Services.AddCors(options =>
{
    var origins = builder.Configuration.GetSection("AllowedHosts").Value?.Split(",");
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins(origins ?? [])
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

Log.Information("Iniciando Web API");
app.Run();