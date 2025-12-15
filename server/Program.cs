using Microsoft.EntityFrameworkCore;
using server.Data;
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
builder.Services.AddScoped<IGiftsRepository, GiftsRepository>();
builder.Services.AddScoped<IDonorsRepository, DonorsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
///services
builder.Services.AddScoped<IGiftsService, GiftsService>();
builder.Services.AddScoped<IDonorsService, DonorsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();





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
