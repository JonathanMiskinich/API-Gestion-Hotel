using HotelManagement.Core.Models;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;

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
    public ActionResult<List<Reserva>> GetAll() => 
        _reservaService.ListarReservas();
    

    [HttpGet("{idCliente}")]
    public ActionResult<Reserva> Get(int idCliente)
    {
        var reserva = _context.Reservas.FirstOrDefault(r => r.IdCliente == idCliente);
        if (reserva == null)
            return NotFound();

        return reserva;
    }

    [HttpPost]
    public ActionResult<Reserva> CrearReserva(Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        _context.SaveChanges();
        return CreatedAtAction(nameof(reserva), new { id = reserva.Id }, reserva);
    }

    [HttpPut("{id}")]
    public ActionResult<Reserva> ModificarReserva(int id, Reserva reserva)
    {
        _reservaService.ModificarReserva(reserva);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult<Reserva> CancelarReserva(int id)
    {
        Reserva? reserva = _context.Reservas.FirstOrDefault(r => r.Id == id);
        Validaciones.ValidarNoNulo(reserva, "Reserva");
        _reservaService.CancelarReserva(reserva);
        return Ok();
    }
}