using HotelManagement.Core.Models;
using HotelManagement.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;


namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/cliente")]
public class ClienteController : ControllerBase
{

    private readonly IClienteService _clienteService;
    private readonly HotelContext _context;

    public ClienteController(IClienteService clienteService, HotelContext context)
    {
        _clienteService = clienteService;
        _context = context;
    }

    [HttpGet] 
    public ActionResult<List<ClienteDTO>> GetAll() => 
        _clienteService.ObtenerClientesActivos();

    [HttpGet("{id}")]
    public ActionResult<ClienteDTO> Get(int id)
    {
        var cliente = _clienteService.ObtenerClientePorId(id);

        if(cliente == null)
            return NotFound();
        
        return cliente;
    }

    [HttpPost]
    public IActionResult Create(CreateClienteDTO cliente)
    {
        _clienteService.RegistrarCliente(cliente);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateClienteDTO clienteActualizado)
    {
        Validaciones.ValidarValorPositivo(id, "ID");
        Validaciones.ValidarNoNulo(clienteActualizado, "Cliente");  

        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
        if(cliente == null)
            return NotFound();
            
        if (id != cliente.Id)
            return BadRequest("El ID de la ruta no coincide con el ID del cliente");
        
        _clienteService.ActualizarCliente(clienteActualizado);
        _context.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _clienteService.EliminarCliente(id);
        _context.SaveChanges();

        return Ok();
    }
}