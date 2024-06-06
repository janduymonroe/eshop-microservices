using EShopMicroservice.Ordering.API;
using EShopMicroservice.Ordering.Application;
using EShopMicroservice.Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddApiServices()
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline

app.Run();
