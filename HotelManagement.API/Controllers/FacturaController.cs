using HotelManagement.Core.Interfaces.Services;
using HotelManagement.Core.Models;
using HotelManagement.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Core.Helpers;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/factura")]
public class FacturaController : ControllerBase
{
    private readonly IFacturaService _facturaService;
    private readonly HotelContext _context;

    public FacturaController(IFacturaService facturaService, HotelContext context)
    {
        _facturaService = facturaService;
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<FacturaDTO>> GetAll() => 
        Ok(_facturaService.ListarFacturas());

    [HttpGet("{id}")]
    public ActionResult<FacturaDTO> GetById(int id) =>
        Ok(_facturaService.ObtenerFacturaPorId(id));

    [HttpPost]
    public ActionResult<FacturaDTO> Create(CreateFacturaDTO facturaDTO)
    {
        var factura = _facturaService.CrearFactura(facturaDTO);
        return Ok("Creada correctamente");
    }

    [HttpPut("{id}")]
    public ActionResult<FacturaDTO> Update(int id, UpdateFacturaDTO facturaDTO)
    {
        if (id != facturaDTO.Id)
            return BadRequest("El ID de la factura no coincide con el ID proporcionado");

        _facturaService.ModificarFactura(facturaDTO);
        return Ok("Modificada correctamente");
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _facturaService.EliminarFactura(id);
        return Ok("Eliminada correctamente");
    }

}