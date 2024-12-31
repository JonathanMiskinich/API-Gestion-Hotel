using HotelManagement.Core.Models;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;
using HotelManagement.Core.DTO;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/reserva")]
public class ReservaController : ControllerBase
{
    private readonly HotelContext _context;
    private readonly IReservaService _reservaService;

    public ReservaController(IReservaService reservaService, HotelContext context)
    {
        _reservaService = reservaService;
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservaDTO>> GetAll() => 
        Ok(_reservaService.ListarReservas());
    

    [HttpGet("obtenerporcliente/{idCliente}")]
    public ActionResult<IEnumerable<ReservaDTO>> Get(int idCliente)
    {
        var reservasDelCliente = _reservaService.ListarReservas(cliente: _context.Clientes.FirstOrDefault(c => c.Id == idCliente));
        return Ok(reservasDelCliente);
    }

    [HttpPost]
    public ActionResult<ReservaDTO> CrearReserva(CreateReservaDTO reserva)
    {
        var reservaDTO = _reservaService.CrearReserva(reserva);
        //return CreatedAtAction(nameof(Get), new { id = reservaDTO.Id }, reservaDTO);
        return Ok(reservaDTO);
    }

    [HttpPut("{id}")]
    public ActionResult<ReservaDTO> ModificarReserva(int id, UpdateReservaDTO reserva)
    {
        _reservaService.ModificarReserva(reserva);
        return Ok();
    }

    [HttpPut("cancelar/{id}")]
    public ActionResult<ReservaDTO> CancelarReserva(int id)
    {
        ReservaDTO? reserva = _reservaService.ObtenerReservaPorId(id);
        Validaciones.ValidarNoNulo(reserva, "Reserva");
        var updateReservaDTO = new UpdateReservaDTO
        {
            FechaInicio = reserva.FechaInicio,
            FechaFin = reserva.FechaFin,
            IdHabitacion = reserva.IdHabitacion,
            IdEstadoReserva = reserva.IdEstadoReserva
        };
        _reservaService.CancelarReserva(updateReservaDTO);
        return Ok(updateReservaDTO);
    }

    [HttpDelete("{id}")]
    public ActionResult<ReservaDTO> EliminarReserva(int id)
    {
        _reservaService.EliminarReserva(id);
        return Ok();
    }
}