using HotelManagement.Core.Interfaces.Services;
using HotelManagement.Core.Models;
using HotelManagement.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/habitacion")]
public class HabitacionController : ControllerBase
{
    private readonly IHabitacionService _habitacionService;
    private readonly HotelContext _context;

    public HabitacionController(IHabitacionService habitacionService, HotelContext context)
    {
        _habitacionService = habitacionService;
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<HabitacionDTO>> GetAll() => 
        Ok(_habitacionService.ListarHabitaciones());

    [HttpGet("disponibles")]
    public ActionResult<IEnumerable<HabitacionDTO>> GetDisponibles(DateOnly fechaInicio, DateOnly fechaFin, Tipohabitacion? tipo = null) => 
        Ok(_habitacionService.ListarHabitacionesDisponibles(fechaInicio, fechaFin, tipo));

    [HttpPost]
    public ActionResult<HabitacionDTO> Post(CreateHabitacionDTO habitacion) => 
        Ok(_habitacionService.CrearHabitacion(habitacion));

    [HttpPut("{id}")]
    public ActionResult<HabitacionDTO> Put(int id, UpdateHabitacionDTO habitacion)
    {
        if (id != habitacion.Id)
            return BadRequest("El ID de la habitacion no coincide con el ID proporcionado");

        _habitacionService.ModificarHabitacion(habitacion);
        return Ok("Modificada Correctamente");
    } 
 
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _habitacionService.EliminarHabitacion(id);
        return Ok("Eliminada Correctamente");
    }
}
