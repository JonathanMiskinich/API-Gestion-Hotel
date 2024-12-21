using HotelManagement.Core.Models;
using HotelManagement.Core.Helpers;

namespace HotelManagement.Infracstructure.Services
{
    public class HabitacionService
    {
        private readonly HotelContext context;

        public  HabitacionService(HotelContext context)
        {
            this.context = context;
        }

        public void RegistrarHabitacion(Habitacione habitacion)
        {
            Validaciones.ValidarNoNulo(habitacion, "habitacion");

            if (context.Habitaciones.Any(h => h.Numero == habitacion.Numero))
                throw new InvalidOperationException("Ese numero de habitacion ya existe.");
            
            context.Habitaciones.Add(habitacion);
        }

        public void ActualizarEstadoHabitacion(Habitacione habitacion, int opcion)
        {
            Validaciones.ValidarNoNulo(habitacion, "habitacion");

            var estadoNuevo = context.Estadohabitacions.SingleOrDefault(e => e.Id == opcion);
            
            if (estadoNuevo == null)
                throw new KeyNotFoundException("La opcion no se encuentra en la base de datos.");
            
            habitacion.CambiarEstado((Estadohabitacion)estadoNuevo);
        }

        public List<Habitacione> ListarHabitaciones(
            int? estado = null, 
            decimal? precioMaximo = null,
            int? tipo = null,
            DateOnly? fechaDeInicio = null,
            DateOnly? fechaDeFin = null)
        {
            var query = context.Habitaciones.AsQueryable();

            if(estado.HasValue && (estado >= 0))
                query = query.Where(h => h.Estado == estado);

            if(precioMaximo.HasValue && (precioMaximo >= 0))
                query = query.Where(h => h.PrecioPorNoche <= precioMaximo);

            if(tipo.HasValue && tipo >= 0)
                query = query.Where(h => h.Tipo == tipo);

            if (fechaDeInicio != null && fechaDeFin != null)
            {
                Validaciones.ValidarRangoFechas((DateOnly)fechaDeInicio, (DateOnly)fechaDeFin);

                query = query.Where(h => !h.Reservas
                    .Any(r => 
                    fechaDeInicio <= r.FECHA_FINALIZACION && 
                    fechaDeFin >= r.FECHA_INICIO));
            } 

            return query.ToList();
        }

        public Habitacione ObtenerHabitacion(int numero)
        {
            Validaciones.ValidarValorPositivo(numero, "numero");

            Habitacione? habitacion = context.Habitaciones.FirstOrDefault(h => h.Numero == numero);
            
            if(habitacion == null)
                throw new KeyNotFoundException($"La habitacion con el numero {numero} no fue encontrada.");
            
            return habitacion;
        }

        public bool EstaHabitacionDisponible(int numeroHabitacion, DateOnly FechaInicio, DateOnly FechaFinalizacion)
        {
            Habitacione habitacion = ObtenerHabitacion(numeroHabitacion);

            Validaciones.ValidarRangoFechas(FechaInicio, FechaFinalizacion);

            return  context.Reservas.Any(r => 
            r.HabitacionNavigation == habitacion && 
            r.FECHA_INICIO < FechaFinalizacion && 
            r.FECHA_FINALIZACION > FechaInicio);

        }

        public Habitacione ObtenerHabitacionPorID(int id)
        {
            return context.Habitaciones.First(h => h.Id == id);
        }
    }
}