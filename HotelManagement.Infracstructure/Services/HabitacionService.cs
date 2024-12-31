using HotelManagement.Core.Models;
using HotelManagement.Core.Helpers;
using AutoMapper;
using HotelManagement.Core.DTO;

namespace HotelManagement.Infracstructure.Services
{
    public class HabitacionService
    {
        private readonly HotelContext context;
        private readonly IMapper mapper;

        public  HabitacionService(HotelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public HabitacionDTO CrearHabitacion(CreateHabitacionDTO habitacionDTO)
        {
            Validaciones.ValidarNoNulo(habitacionDTO, "habitacion");

            var habitacionEntidad = mapper.Map<Habitacione>(habitacionDTO);
            
            context.Habitaciones.Add(habitacionEntidad);
            context.SaveChanges();

            return mapper.Map<HabitacionDTO>(habitacionEntidad);
        }

        public HabitacionDTO ObtenerHabitacionPorId(int id)
        {
            Validaciones.ValidarValorPositivo(id, "id");

            var habitacion = context.Habitaciones.FirstOrDefault(h => h.Id == id);

            Validaciones.ValidarNoNulo(habitacion, "habitacion");

            return mapper.Map<HabitacionDTO>(habitacion);
        }

        public void ModificarHabitacion(UpdateHabitacionDTO habitacionDTO)
        {
            Validaciones.ValidarNoNulo(habitacionDTO, "habitacion");

            var habitacionEntidad = mapper.Map<Habitacione>(habitacionDTO);

            context.Habitaciones.Update(habitacionEntidad);
        }

        public void EliminarHabitacion(int id)
        {
            Validaciones.ValidarValorPositivo(id, "id");

            var habitacion = context.Habitaciones.FirstOrDefault(h => h.Id == id);

            Validaciones.ValidarNoNulo(habitacion, "habitacion");

            context.Habitaciones.Remove(habitacion);
            context.SaveChanges();
        }


        public void CambiarEstadoHabitacion(int numeroHabitacion, Estadohabitacion nuevoEstado)
        {
            Validaciones.ValidarValorPositivo(numeroHabitacion, "numeroHabitacion");
            Validaciones.ValidarNoNulo(nuevoEstado, "nuevoEstado");

            var habitacion = ObtenerHabitacion(numeroHabitacion);
            Validaciones.ValidarNoNulo(habitacion, "habitacion");

            var habitacionUpdate = new UpdateHabitacionDTO
            {
                Numero = numeroHabitacion,
                Estado = nuevoEstado.Id,
                PrecioPorNoche = habitacion.PrecioPorNoche,
                Tipo = habitacion.Tipo
            }; 

            ModificarHabitacion(habitacionUpdate);
        }

        public IEnumerable<HabitacionDTO> ListarHabitaciones(
            Tipohabitacion? tipo = null,
            int? numeroHabitacion = null,
            decimal? precioMinimo = null,
            decimal? precioMaximo = null
        )
        {
            var query = context.Habitaciones.AsQueryable();

            if(numeroHabitacion.HasValue && (numeroHabitacion >= 0))
                query = query.Where(h => h.Numero == numeroHabitacion);

            if(precioMaximo.HasValue && (precioMaximo >= 0))
                query = query.Where(h => h.PrecioPorNoche <= precioMaximo);

            if(precioMinimo.HasValue && (precioMinimo >= 0))
                query = query.Where(h => h.PrecioPorNoche >= precioMinimo);

            if (tipo != null)
                query = query.Where(h => h.Tipo == tipo.Id);

            return query.ToList().Select(h => mapper.Map<HabitacionDTO>(h));
        }

        public IEnumerable<HabitacionDTO> ListarHabitacionesDisponibles(
            DateOnly fechaInicio, 
            DateOnly fechaFin, 
            Tipohabitacion? tipo = null)
        {
            var query = context.Habitaciones.AsQueryable();

            if(tipo != null)
                query = query.Where(h => h.Tipo == tipo.Id);

            query = query.Where(h => EstaHabitacionDisponible(h.Numero, fechaInicio, fechaFin));

            return query.ToList().ConvertAll(h => mapper.Map<HabitacionDTO>(h));
        }
        public bool EstaHabitacionDisponible(int numeroHabitacion, DateOnly FechaInicio, DateOnly FechaFinalizacion)
        {
            Habitacione? habitacion = ObtenerHabitacion(numeroHabitacion);

            Validaciones.ValidarNoNulo(habitacion, "habitacion");
            Validaciones.ValidarRangoFechas(FechaInicio, FechaFinalizacion);

            return  context.Reservas.Any(r => 
            r.HabitacionNavigation == habitacion && 
            r.FECHA_INICIO < FechaFinalizacion && 
            r.FECHA_FINALIZACION > FechaInicio);

        }

        public Habitacione? ObtenerHabitacion(int numeroHabitacion)
        {
            return context.Habitaciones.FirstOrDefault(h => h.Numero == numeroHabitacion);
        }
    }
}