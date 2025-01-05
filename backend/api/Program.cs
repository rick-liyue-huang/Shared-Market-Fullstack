using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Interfaces;
using api.Repository;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); // create a PostgreSQL connection
});

builder.Services.AddScoped<IStockRepository, StockRepository>(); // add the StockRepository service
builder.Services.AddScoped<ICommentRepository, CommentRepository>(); // add the CommentRepository service

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
