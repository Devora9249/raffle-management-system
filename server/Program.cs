using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Services.Implementations;
using server.Services.Interfaces;
using server.Repositories.Implementations;
using server.Repositories.Interfaces;



// try
// {
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//DI
///repositories
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
///services
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();





builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();
// }
// catch (Exception ex)
// {
//     Log.Fatal(ex, "Application terminated unexpectedly");
// }
// finally
// {
//     Log.CloseAndFlush();
// }
