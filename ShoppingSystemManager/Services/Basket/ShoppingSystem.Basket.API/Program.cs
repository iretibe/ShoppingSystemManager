using Carter;
using Marten;
using ShoppingSystem.Basket.API.Models;
using ShoppingSystem.Basket.API.Repositories;
using ShoppingSystem.BuildingBlocks.Behaviors;
using ShoppingSystem.BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("PostGreSqlDatabase"));
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
