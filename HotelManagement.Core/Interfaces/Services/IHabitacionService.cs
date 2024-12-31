using HotelManagement.Core.DTO;
using HotelManagement.Core.Models;

namespace HotelManagement.Core.Interfaces.Services;

public interface IHabitacionService
{
    // CRUD
    HabitacionDTO CrearHabitacion(CreateHabitacionDTO habitacion);
    HabitacionDTO? ObtenerHabitacionPorId(int id);
    void ModificarHabitacion(UpdateHabitacionDTO habitacion);
    void EliminarHabitacion(int idHabitacion);
    //Operaciones de negocio
    IEnumerable<HabitacionDTO> ListarHabitaciones(
        Tipohabitacion? tipo = null, 
        int? numeroHabitacion = null,
        decimal? precioMinimo = null,
        decimal? precioMaximo = null
        );
    
    IEnumerable<HabitacionDTO> ListarHabitacionesDisponibles(
        DateOnly fechaInicio,
        DateOnly fechaFin,
        Tipohabitacion? tipo = null
    );

    void CambiarEstadoHabitacion(int numeroHabitacion, Estadohabitacion nuevoEstado);
}