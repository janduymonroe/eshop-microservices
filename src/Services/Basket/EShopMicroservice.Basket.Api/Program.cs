using EShopMicroservice.Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BuildingBlocks.Messaging.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add service to the container

// Application Services
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

var database = builder.Configuration.GetConnectionString("Database")!;
var redis = builder.Configuration.GetConnectionString("Redis")!;

builder.Services.AddValidatorsFromAssembly(assembly);

// Data Services
builder.Services.AddMarten(opt =>
{
    opt.Connection(database);
    opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redis;
});

// gRPC Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
    .ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

// Async Comunication Services
builder.Services.AddMessageBroker(builder.Configuration);

// Cross-Cutting services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(database)
    .AddRedis(redis);

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(opt => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
