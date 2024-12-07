using HotelManagement.Core.Models;

namespace HotelManagement.Services
{
    public class ChechInOut
    {
        private readonly HotelContext context;

        public ChechInOut(HotelContext context)
        {
            this.context = context;
        }

        public void RegistrarCheckIn(int idReserva)
        {
            Reserva reserva;
            try
            {
                reserva = context.Reservas.First(r => r.Id == idReserva);
            }
            catch (Exception)
            {
                throw new Exception($"El id: {idReserva}  no corresponder a ninguna reserva.");
            }

            if(reserva.EstadoReservaNavigation.Descripcion.ToLower() != "pendiente")
                throw new InvalidOperationException($"La reserva ya esta {reserva.EstadoReservaNavigation.Descripcion}");
            
            new ReservaService(context).ModificarReserva(reserva, estadoNuevo: context.EstadoReservas.FirstOrDefault(e => e.Descripcion.ToLower() == "confirmada"));

            if(reserva.FECHA_INICIO != DateOnly.FromDateTime(DateTime.Now))
                throw new InvalidOperationException("La fecha de inicio de la reserva no es hoy.");
            
            try
            {
                new HabitacionService(context).ActualizarEstadoHabitacion(reserva.HabitacionNavigation, context.Estadohabitacions.First(e => e.Descripcion.ToLower() == "ocupada").Id);
            }
            catch (Exception ex)
            {
                throw new  Exception("Error", ex);
            }

        }

        public void CheckOut(int idReserva)
        {
            Reserva reserva;
            try
            {
                reserva = context.Reservas.First(r => r.Id == idReserva);
            }
            catch (Exception)
            {
                throw new Exception($"El id: {idReserva}  no corresponder a ninguna reserva.");
            }

            new ReservaService(context).ModificarReserva(reserva, estadoNuevo: context.EstadoReservas.First(e => e.Descripcion.ToLower() == "Finalizada"));
            new HabitacionService(context).ActualizarEstadoHabitacion(reserva.HabitacionNavigation, context.Estadohabitacions.First(e => e.Descripcion.ToLower() == "disponible").Id);

            reserva.AgregarFactura(new FacturaService(context).GenerarFacturaReserva(reserva));
        }
    }
}