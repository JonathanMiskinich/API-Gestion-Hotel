using HotelManagement.Core.Models;
using HotelManagement.Core.Helpers;

namespace HotelManagement.Infracstructure.Services
{
    public class FacturaService
    {
        private readonly HotelContext context;

        public FacturaService(HotelContext context)
        {
            this.context = context;
        }

        public Factura GenerarFacturaReserva(Reserva reserva)
        {
            Validaciones.ValidarNoNulo(reserva, "Reserva");

            decimal MontoTotal = HelperPrecio.CalcularPrecio(reserva);

            return CrearFactura(reserva, MontoTotal);
        }

        public List<Factura> ObtenerFacturasCliente(
            Cliente cliente,
            DateOnly? DesdeFecha = null,
            DateOnly? HastaFecha = null,
            Tipohabitacion? tipo = null)
        {
            Validaciones.ValidarNoNulo(cliente, "cliente");

            var query = context.Facturas.AsQueryable()
                        .Where(f => f.IdCliente == cliente.Id);

            if(DesdeFecha.HasValue)
                query = query.Where(f => f.FechaEmision >= DesdeFecha );
            if(HastaFecha.HasValue)
                query = query.Where(f => f.FechaEmision <= HastaFecha );
            if(tipo != null)
                query = query.Where(f => f.ReservaNavigation.HabitacionNavigation.TipoNavigation == tipo);
            
            return query.ToList();
        }

        public void AplicarDescuentoAFactura(Factura factura, decimal descuento)
        {
            Validaciones.ValidarNoNulo(factura, nameof(factura));

            factura.MONTO_TOTAL = HelperPrecio.CalcularPrecio(factura.ReservaNavigation, descuento);
        }

        private Factura CrearFactura(Reserva reserva, decimal montoTotal)
        {
            return new Factura(
                (int)reserva.IdCliente,
                reserva.Id,
                montoTotal,
                reserva.ClienteNavigation,
                reserva);
        }
    }
}