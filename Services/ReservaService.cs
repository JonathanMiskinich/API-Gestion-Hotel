using HotelManagement.Models;
using HotelManagement.Helpers;

namespace HotelManagement.Services
{
    public class ReservaService
    {
        private readonly HotelContext context;

        public ReservaService(HotelContext context)
        {
            this.context = context;
        }

        public Reserva CrearReserva(int IdCliente, int numeroHabitacion, DateOnly FechaInicioReserva, DateOnly fechaFinReserva)
        {
            Validaciones.ValidarValorPositivo(IdCliente, "ID cliente");
            
            Validaciones.ValidarValorPositivo(numeroHabitacion, "Numero de la habitacion");

            int idHabitacion = (int)new HabitacionService(context).ObtenerHabitacion(numeroHabitacion).Id;

            if(!new HabitacionService(context).EstaHabitacionDisponible(numeroHabitacion, FechaInicioReserva, fechaFinReserva))
                throw new Exception("La habitacion no esta disponible.");

            EstadoReserva? estado = context.EstadoReservas.FirstOrDefault(e => e.Descripcion.ToLower() == "pendiente");

            Validaciones.ValidarNoNulo(estado, "estado");
            
            return CrearReserva(IdCliente, idHabitacion, estado, FechaInicioReserva, fechaFinReserva);
        }

        public List<Reserva> ListarReservas(
            EstadoReserva? estado = null,
            Cliente? cliente = null,
            Tipohabitacion? tipo = null,
            int? numeroHabitacion = null)
        {
            var query = context.Reservas.AsQueryable();

            if(estado != null)
                query = query.Where(r => r.EstadoReservaNavigation.Descripcion == estado.Descripcion);
            if(cliente != null)
                query = query.Where(r => r.IdCliente == cliente.Id);
            if(tipo != null)
                query = query.Where(r => r.IdEstadoReserva == tipo.Id);
            if(numeroHabitacion != null)
                query = query.Where(r => r.HabitacionNavigation.Numero == numeroHabitacion);
            
            return query.ToList();
        }

        public void ModificarReserva(Reserva reserva, 
        DateOnly? FechaNuevaInicio = null,
        DateOnly? FechaNuevaFin = null,
        EstadoReserva? estadoNuevo = null)
        {
            if(estadoNuevo != null)
            {
                reserva.EstadoReservaNavigation = estadoNuevo;
                reserva.IdEstadoReserva = estadoNuevo.Id;
            }
            
            HabitacionService service = new(context);

            if(FechaNuevaFin.HasValue && FechaNuevaInicio.HasValue)
            {
                if(!(service.EstaHabitacionDisponible(reserva.HabitacionNavigation.Numero, (DateOnly)FechaNuevaInicio, (DateOnly)FechaNuevaFin)))
                    throw new InvalidOperationException("La habitacion no esta disponible en esa fecha.");
                
                reserva.FECHA_INICIO = (DateOnly)FechaNuevaInicio;
                reserva.FECHA_FINALIZACION = (DateOnly)FechaNuevaInicio; 
            }

            if(FechaNuevaInicio.HasValue)
            {
                if(service.EstaHabitacionDisponible(reserva.HabitacionNavigation.Numero, (DateOnly)FechaNuevaInicio, (DateOnly)FechaNuevaInicio))
                   reserva.FECHA_INICIO = (DateOnly)FechaNuevaInicio; 
            }

            if(FechaNuevaFin.HasValue)
            {
                if(service.EstaHabitacionDisponible(reserva.HabitacionNavigation.Numero, (DateOnly)FechaNuevaFin, (DateOnly)FechaNuevaFin))
                    reserva.FECHA_FINALIZACION = (DateOnly)FechaNuevaFin;  
            }
        }

        public void CancelarReserva(Reserva reserva)
        {
            EstadoReserva? estado = context.EstadoReservas.FirstOrDefault(r => r.Descripcion.ToLower() == "cancelada");

            Validaciones.ValidarNoNulo(estado, "estado");

            ModificarReserva(reserva, estadoNuevo: estado);
        }

        private Reserva CrearReserva(int idClienteDeLaReserva, int idHabitacionDeLaReserva, EstadoReserva estado, DateOnly FechaInicioReserva, DateOnly fechaFinReserva)
        {
            Cliente? cliente = new ClienteService(context).ObtenerClientePorID(idClienteDeLaReserva);
            Validaciones.ValidarNoNulo(cliente , "cliente");

            return new Reserva(
                idClienteDeLaReserva,
                idHabitacionDeLaReserva,
                FechaInicioReserva,
                fechaFinReserva,
                estado.Id,
                cliente,
                new HabitacionService(context).ObtenerHabitacionPorID(idHabitacionDeLaReserva),
                estado);
        }
    }
}