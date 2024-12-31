using HotelManagement.Core.Models;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;
using AutoMapper;
using HotelManagement.Core.DTO;

namespace HotelManagement.Infracstructure.Services
{
    public class ReservaService : IReservaService
    {
        private readonly HotelContext context;
        private readonly IMapper mapper;

        public ReservaService(HotelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ReservaDTO CrearReserva(CreateReservaDTO reservaDTO)
        {
            var reservaEntidad = mapper.Map<Reserva>(reservaDTO);
            context.Reservas.Add(reservaEntidad);

            return mapper.Map<ReservaDTO>(reservaEntidad);
        }

        public IEnumerable<ReservaDTO> ListarReservas(
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
            
            return query.ToList().Select(r => mapper.Map<ReservaDTO>(r));
        }

        public ReservaDTO? ObtenerReservaPorId(int id)
        {
            var reserva = context.Reservas.FirstOrDefault(r => r.Id == id);

            return mapper.Map<ReservaDTO>(reserva);
        }

        public void EliminarReserva(int idReserva)
        {
            Validaciones.ValidarValorPositivo(idReserva, "id");

            var reservaEntidad = context.Reservas.FirstOrDefault(r => r.Id == idReserva);
            Validaciones.ValidarNoNulo(reservaEntidad, "reserva");

            context.Reservas.Remove(reservaEntidad);
            context.SaveChanges();
        }

        public void ModificarReserva(UpdateReservaDTO reservaDTO)
        {
            var reserva = context.Reservas.FirstOrDefault(r => r.Id == reservaDTO.Id);
            Validaciones.ValidarNoNulo(reserva, "reserva");

            reserva.FECHA_INICIO = reservaDTO.FechaInicio;
            reserva.FECHA_FINALIZACION = reservaDTO.FechaFin;
            reserva.IdHabitacion = reservaDTO.IdHabitacion;
            reserva.IdEstadoReserva = reservaDTO.IdEstadoReserva;

            context.Reservas.Update(reserva);
            context.SaveChanges();
        }

        public void ConfirmarReserva(UpdateReservaDTO reservaDTO)
        {
            var estadoReservaConfirmada = context.EstadoReservas.FirstOrDefault(e => e.Descripcion.ToLower() == "confirmada");
            Validaciones.ValidarNoNulo(estadoReservaConfirmada, "estado de reserva");

            if (reservaDTO.IdEstadoReserva != estadoReservaConfirmada.Id)
                throw new InvalidOperationException("El estado de la reserva no es confirmada.");

            ModificarReserva(reservaDTO);
        }

        public void CancelarReserva(UpdateReservaDTO reservaDTO)
        {
            var estadoReservaCancelada = context.EstadoReservas.FirstOrDefault(e => e.Descripcion.ToLower() == "cancelada");
            Validaciones.ValidarNoNulo(estadoReservaCancelada, "estado de reserva");

            if (reservaDTO.IdEstadoReserva != estadoReservaCancelada.Id)
                throw new InvalidOperationException("El estado de la reserva no es cancelada.");

            ModificarReserva(reservaDTO);
        }

        // Metodos de ayuda
        private Habitacione ObtenerYValidarHabitacion(int numeroHabitacion, DateOnly fechaInicio, DateOnly fechaFin)
        {
            Validaciones.ValidarValorPositivo(numeroHabitacion, "numero de habitacion");

            var habitacion = new HabitacionService(context, mapper).ObtenerHabitacion(numeroHabitacion);

            Validaciones.ValidarNoNulo(habitacion, "habitacion");
            Validaciones.ValidarRangoFechas(fechaInicio, fechaFin);

            if(!new HabitacionService(context, mapper).EstaHabitacionDisponible(numeroHabitacion, fechaInicio, fechaFin))
                throw new InvalidOperationException("La habitacion no esta disponible.");

            return habitacion;
        }

        private Cliente ObtenerYValidarCliente(int idCliente)
        {
            Validaciones.ValidarValorPositivo(idCliente, "id de cliente");

            var cliente = new ClienteService(context, mapper).ObtenerClientePorId(idCliente);

            Validaciones.ValidarNoNulo(cliente, "cliente");

            return mapper.Map<Cliente>(cliente);
        }

        private EstadoReserva ObtenerEstadoPendiente()
        {
            var estado = context.EstadoReservas.FirstOrDefault(e => e.Descripcion.ToLower() == "pendiente");
            Validaciones.ValidarNoNulo(estado, "estado");
            return estado;
        }
    }
}