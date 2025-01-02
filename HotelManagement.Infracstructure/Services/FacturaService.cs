using HotelManagement.Core.Models;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;
using HotelManagement.Core.DTO;
using AutoMapper;

namespace HotelManagement.Infracstructure.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly HotelContext context;
        private readonly IMapper mapper;

        public FacturaService(HotelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public FacturaDTO CrearFactura(CreateFacturaDTO facturaDTO)
        {
            Validaciones.ValidarNoNulo(facturaDTO, "Factura");

            var facturaEntidad = mapper.Map<Factura>(facturaDTO);
            context.Facturas.Add(facturaEntidad);
            context.SaveChanges();

            return mapper.Map<FacturaDTO>(facturaEntidad);
        }

        public FacturaDTO? ObtenerFacturaPorId(int id)
        {
            Validaciones.ValidarValorPositivo(id, "Id");

            var factura = context.Facturas.FirstOrDefault(f => f.Id == id);

            Validaciones.ValidarNoNulo(factura, "Factura");

            return mapper.Map<FacturaDTO>(factura);
        }

        public void ModificarFactura(UpdateFacturaDTO facturaDTO)
        {
            Validaciones.ValidarNoNulo(facturaDTO, "Factura");
            var facturaEntidad = context.Facturas.FirstOrDefault(f => f.Id == facturaDTO.Id);

            Validaciones.ValidarNoNulo(facturaEntidad, "Factura");
            facturaEntidad.MONTO_TOTAL = facturaDTO.MontoTotal;

            context.Facturas.Update(facturaEntidad);
            context.SaveChanges();
        }

        public void EliminarFactura(int idFactura)
        {
            Validaciones.ValidarValorPositivo(idFactura, "Id");

            var factura = context.Facturas.FirstOrDefault(f => f.Id == idFactura);

            Validaciones.ValidarNoNulo(factura, "Factura");

            context.Facturas.Remove(factura);
            context.SaveChanges();
        }

        public FacturaDTO GenerarFacturaDesdeReserva(int idReserva)
        {
            Validaciones.ValidarValorPositivo(idReserva, "Id");

            var reserva = context.Reservas.FirstOrDefault(r => r.Id == idReserva);

            Validaciones.ValidarNoNulo(reserva, "Reserva");

            decimal MontoTotalCalculado = HelperPrecio.CalcularPrecio(reserva);

            var facturaDTO = new CreateFacturaDTO
            {
                IdCliente = reserva.IdCliente,
                IdReserva = idReserva,
                MontoTotal = MontoTotalCalculado
            };

            return CrearFactura(facturaDTO);
        }

        public FacturaDTO ObtenerFacturaPorReserva(int idReserva)
        {
            Validaciones.ValidarValorPositivo(idReserva, "Id");

            var factura = context.Facturas.FirstOrDefault(f => f.IdReserva == idReserva);

            Validaciones.ValidarNoNulo(factura, "Factura");

            return mapper.Map<FacturaDTO>(factura);
        }


        public IEnumerable<FacturaDTO> ListarFacturas(
            DateOnly? DesdeFecha = null,
            DateOnly? HastaFecha = null,
            int? idCliente = null,
            decimal? montoMinimo = null,
            decimal? montoMaximo = null
        )
        {
            var query = context.Facturas.AsQueryable();

            if(DesdeFecha.HasValue)
                query = query.Where(f => f.FechaEmision >= DesdeFecha );
            if(HastaFecha.HasValue)
                query = query.Where(f => f.FechaEmision <= HastaFecha );

            if(idCliente.HasValue)
                query = query.Where(f => f.IdCliente == idCliente);

            if(montoMinimo.HasValue)
                query = query.Where(f => f.MONTO_TOTAL >= montoMinimo);

            if(montoMaximo.HasValue)
                query = query.Where(f => f.MONTO_TOTAL <= montoMaximo);

            return query.ToList().Select(f => mapper.Map<FacturaDTO>(f));
        }
    }
}