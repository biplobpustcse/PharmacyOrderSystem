using Microsoft.EntityFrameworkCore;
using PharmacyOrderSystem.Application.Interfaces;
using PharmacyOrderSystem.Application.MappingProfiles;
using PharmacyOrderSystem.Application.Services;
using PharmacyOrderSystem.Domain.Entities;
using PharmacyOrderSystem.Infrastructure.Persistence;
using PharmacyOrderSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// dependecy injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<IRepository<Customer>, Repository<Customer>>();
builder.Services.AddScoped<IRepository<Medicine>, Repository<Medicine>>();
builder.Services.AddScoped<OrderService>();


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
