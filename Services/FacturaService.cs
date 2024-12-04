using HotelManagement.Models;
using HotelManagement.Helpers;

namespace HotelManagement.Services
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
            if(reserva == null)
                throw new ArgumentNullException("No se le puede generar una factura a una reserva vacia.");
            decimal MontoTotal = HelperPrecio.CalcularPrecio(reserva);

            Factura facturaGenerada = new Factura(
                (int)reserva.IdCliente,
                reserva.Id,
                MontoTotal,
                reserva.ClienteNavigation,
                reserva);
            
            return facturaGenerada;
        }

        public List<Factura> ObtenerFacturasCliente(
            Cliente cliente,
            DateOnly? DesdeFecha = null,
            DateOnly? HastaFecha = null,
            Tipohabitacion? tipo = null)
        {
            if(cliente == null)
                throw new ArgumentNullException("NO se permite un cliente nulo.");

            var query = context.Facturas.AsQueryable();
            
            query = query.Where(f => f.IdCliente == cliente.Id);

            if(DesdeFecha != null)
                query = query.Where(f => f.FechaEmision >= DesdeFecha );
            if(HastaFecha != null)
                query = query.Where(f => f.FechaEmision <= HastaFecha );
            if(tipo != null)
                query = query.Where(f => f.ReservaNavigation.HabitacionNavigation.TipoNavigation == tipo);
            
            return query.ToList();
        }

        public void AplicarDescuentoAFactura(Factura factura, decimal descuento)
        {
            factura.MONTO_TOTAL = HelperPrecio.CalcularPrecio(factura.ReservaNavigation, descuento);
        }
    }
}