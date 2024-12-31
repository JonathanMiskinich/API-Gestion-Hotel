using HotelManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Services;
using HotelManagement.Core.Helpers;
using HotelManagement.Infracstructure.Services;
using HotelManagement.Core.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("HotelDataBase");
Validaciones.ValidarNoNulo(connectionString, "Conexion a la base de datos");

// Registra el DbContext con el proveedor MySQL
builder.Services.AddDbContext<HotelContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30))));

builder.Services.AddControllers();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
