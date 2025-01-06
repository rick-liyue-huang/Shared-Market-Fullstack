using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using api.Data;
using api.Interfaces;
using api.Repositories;
using api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers(); // for .net web api mvc 

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme =
  options.DefaultChallengeScheme =
  options.DefaultForbidScheme =
  options.DefaultScheme =
  options.DefaultSignInScheme =
  options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWT:Audience"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(
          System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
      )
  };
});




builder.Services.AddScoped<IStockRepository, StockRepository>(); // add services to the container
builder.Services.AddScoped<ICommentRepository, CommentRepository>(); // add services to the container

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ADD AUTH 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // match with builder.Services.AddControllers();


app.Run();
