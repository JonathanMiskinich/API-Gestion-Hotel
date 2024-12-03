using HotelManagement.Models;

namespace Services.Habitaciones
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
            if (context.Habitaciones.Any(h => h.Numero == habitacion.Numero))
                throw new Exception("Ese numero de habitacion ya existe.");
            if(habitacion == null)
                throw new ArgumentNullException("No se permite agregar un elemento nulo.");
            
            context.Habitaciones.Add(habitacion);
        }

        public void ActualizarEstadoHabitacion(Habitacione habitacion, int opcion)
        {
            if(habitacion == null)
                throw new ArgumentNullException("La habitacion no puede ser nula.");

            var estadoNuevo = context.Estadohabitacions.SingleOrDefault(e => e.Id == opcion);
            
            if (estadoNuevo == null)
                throw new KeyNotFoundException("La opcion no se encuentra en la base de datos.");
            
            try
            {
                habitacion.Estado = opcion;
                habitacion.CambiarEstado((Estadohabitacion)estadoNuevo);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se puedo realizar la accion.", ex);
            }
        }

        public List<Habitacione> ListarHabitaciones(
            int? estado = null, 
            decimal? precioMaximo = null,
            int? tipo = null,
            DateOnly? fechaDeInicio = null,
            DateOnly? fechaDeFin = null)
        {
            var query = context.Habitaciones.AsQueryable();

            if(estado.HasValue && !(estado < 0))
                query = query.Where(h => h.Estado == estado);

            if(precioMaximo.HasValue && !(precioMaximo < 0))
                query = query.Where(h => h.PrecioPorNoche <= precioMaximo);

            if(tipo.HasValue && tipo >= 0)
                query = query.Where(h => h.Tipo == tipo);

            if (fechaDeInicio != null && fechaDeFin != null)
            {
                query.Where(h => !h.Reservas
                    .Any(r => fechaDeInicio <= r.FechaFinalizacion && fechaDeFin >= r.FechaInicio)
                );
            } 

            return query.ToList();
        }

        public Habitacione ObtenerHabitacion(int numero)
        {
            if(numero < 0)
                throw new ArgumentOutOfRangeException("Numero de habitacion ingresado invalido.");

            return context.Habitaciones.FirstOrDefault(h => h.Numero == numero);
        }
    }
}