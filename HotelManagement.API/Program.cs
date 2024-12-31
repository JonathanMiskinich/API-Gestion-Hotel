using HotelManagement.Infracstructure.Services;
using HotelManagement.Core.Models;
using HotelManagement.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Mappers;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("HotelDataBase");
Validaciones.ValidarNoNulo(connectionString, "Conexion a la base de datos");

builder.Services.AddDbContext<HotelContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30))));


builder.Services.AddControllers();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
