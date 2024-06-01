var builder = WebApplication.CreateBuilder(args);

// Add service to the container
builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.Run();
