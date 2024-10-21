using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Discount.Grpc.Data;
using ShoppingSystem.Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
