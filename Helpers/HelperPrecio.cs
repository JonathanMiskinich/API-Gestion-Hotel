using HotelManagement.Models;

namespace HotelManagement.Helpers
{
    public class HelperPrecio
    {
        public static decimal CalcularPrecio(Reserva reserva, decimal? descuento = null)
        {
            decimal precioPorNoche = reserva.HabitacionNavigation.PrecioPorNoche;
            decimal MontoTotal = precioPorNoche * (reserva.FechaFinalizacion.ToDateTime(TimeOnly.MinValue) - reserva.FechaInicio.ToDateTime(TimeOnly.MinValue)).Days;
            if(descuento != null)
                return MontoTotal * (1 - (decimal)descuento);
            return MontoTotal;
        }   
    }
}