using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyOrderSystem.Application.Interfaces;
using PharmacyOrderSystem.Application.MappingProfiles;
using PharmacyOrderSystem.Application.Services;
using PharmacyOrderSystem.Application.Settings;
using PharmacyOrderSystem.Infrastructure.Persistence;
using PharmacyOrderSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
);

// Bind JWTSettings
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

// Add Authentication and configure JWT Bearer
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SecretKey"])
            ),
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
        };
    });

// dependecy injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
