using HotelManagement.Core.DTO;
using HotelManagement.Core.Models;

namespace HotelManagement.Core.Interfaces.Services;

public interface IFacturaService
{
    // CRUD
    FacturaDTO CrearFactura(CreateFacturaDTO factura);
    FacturaDTO? ObtenerFacturaPorId(int id);
    void ModificarFactura(UpdateFacturaDTO factura);
    void EliminarFactura(int idFactura);

    // Operaciones de negocio
    //void PagarFactura(int idFactura);
    FacturaDTO ObtenerFacturaPorReserva(int idReserva);
    FacturaDTO GenerarFacturaDesdeReserva(int idReserva);

    // Listados
    IEnumerable<FacturaDTO> ListarFacturas(
        DateOnly? DesdeFecha = null,
        DateOnly? HastaFecha = null,
        int? idCliente = null,
        decimal? montoMinimo = null,
        decimal? montoMaximo = null
    );
}