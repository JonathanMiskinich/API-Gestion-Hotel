using HotelManagement.Core.Models;
using HotelManagement.Infracstructure.Services;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;


namespace HotelManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{

    private readonly IClienteService _clienteService;
    private readonly HotelContext _context;

    public ClienteController(IClienteService clienteService, HotelContext context   )
    {
        _clienteService = clienteService;
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Cliente>> GetAll() => 
        _clienteService.ObtenerClientesActivos();

    [HttpGet("{id}")]
    public ActionResult<Cliente> Get(int id)
    {
        var cliente = _clienteService.ObtenerClientePorId(id);

        if(cliente == null)
            return NotFound();
        
        return cliente;
    }

    [HttpPost]
    public IActionResult Create(Cliente cliente)
    {
        _clienteService.RegistrarCliente(cliente);
        _context.SaveChanges();

        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Cliente clienteActualizado)
    {
        if (id != clienteActualizado.Id)
            return BadRequest();

        var cliente = _clienteService.ObtenerClientePorId(id);
        
        _clienteService.ActualizarCliente(clienteActualizado);
        _context.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _clienteService.EliminarCliente(id);
        _context.SaveChanges();

        return Ok();
    }
}