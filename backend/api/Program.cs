using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Interfaces;
using api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers(); // add the MVC service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); // create a PostgreSQL connection
});

builder.Services.AddScoped<IStockRepository, StockRepository>(); // add the StockRepository service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // add the MVC middleware

app.Run();
