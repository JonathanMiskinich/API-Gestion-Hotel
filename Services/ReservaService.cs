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

        public Reserva CrearReserva(int IdCliente, int idHabitacion, DateOnly FechaInicioReserva, DateOnly fechaFinReserva)
        {
            if(IdCliente < 0)
                throw new ArgumentOutOfRangeException("El Id del CLiente no puede ser nulo");
            Cliente clienteDeLaReserva;
            try
            {
                clienteDeLaReserva = context.Clientes.First(c => c.Id == IdCliente);
            }
            catch (Exception)
            {
                throw new ArgumentNullException($"No se encontro al cliente con el id {IdCliente}");
            }

            if (idHabitacion < 0)
                throw new ArgumentOutOfRangeException("No se permiten Id de habitacion negativos");
            Habitacione habitacionDeLaReserva;
            try
            {
                habitacionDeLaReserva = context.Habitaciones.First(h => h.Id == idHabitacion);
            }
            catch (Exception)
            {
                throw new ArgumentNullException($"No se encontro la habitacion con el id {idHabitacion}");
            }


            if(!new HabitacionService(context).EstaHabitacionDisponible((int)habitacionDeLaReserva.Numero, FechaInicioReserva, fechaFinReserva))
                throw new Exception("La habitacion no esta disponible.");

            EstadoReserva estado = context.EstadoReservas.FirstOrDefault(e => e.Descripcion.ToLower() == "pendiente");
            Reserva reserva = new Reserva(
                IdCliente,
                idHabitacion,
                FechaInicioReserva,
                fechaFinReserva,
                estado.Id,
                clienteDeLaReserva,
                habitacionDeLaReserva,
                estado);
            
            return reserva;
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

            if(FechaNuevaFin != null)
                reserva.FECHA_FINALIZACION = (DateOnly)FechaNuevaFin;

            if(FechaNuevaInicio != null)
                reserva.FECHA_INICIO = (DateOnly)FechaNuevaInicio;

        }

        public void CancelarReserva(Reserva reserva)
        {
            EstadoReserva estado = context.EstadoReservas.FirstOrDefault(r => r.Descripcion.ToLower() == "cancelada");

            reserva.IdEstadoReserva = estado.Id;
            reserva.EstadoReservaNavigation = estado;
        }
    }
}