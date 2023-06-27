using BeerWebAPI.Service.Interface;
using BeerWebAPI.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;
using BeerWebAPI.DataAccess.Repository;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.Service.Services;
using BeerWebAPI.DataAccess.DbModel;
using BeerWebAPI.Middleware;
using BeerWebAPI.DataAccess.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IBreweryService, BreweryService>();
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IBarService, BarService>();
builder.Services.AddScoped<IBreweryBeerService, BreweryBeerService>();
builder.Services.AddScoped<IBarBeerService, BarBeerService>();

builder.Services.AddScoped<IRepository<BeerDBModel>, BeerRepository>();
builder.Services.AddScoped<IRepository<BreweryDBModel>, BreweryRepository>();
builder.Services.AddScoped<IRepository<BarDBModel>, BarRepository>();
builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddScoped<IRelationalRepository<BarBeerDBModel, BarBeerResponseModel>, BarBeerRepository>();
builder.Services.AddScoped<IRelationalRepository<BreweryBeerDBModel, BreweryBeerResponseModel>, BreweryBeerRepository>();

builder.Services.AddOptions();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configure serilog for global logging.
var _logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(_logger);

var app = builder.Build();
var loggerFactory = app.Services.GetService<ILoggerFactory>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Swagger API documentation.
    app.UseSwagger();
    app.UseSwaggerUI();
}
//global exception handler
app.ExceptionHandler();

app.UseAuthorization();
app.MapControllers();
app.Run();