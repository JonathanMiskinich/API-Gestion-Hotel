using HotelManagement.Core.Models;
using HotelManagement.Core.DTO;

namespace HotelManagement.Core.Interfaces.Services;

public interface IReservaService
{
    // CRUD basico
    ReservaDTO CrearReserva(CreateReservaDTO reserva);
    void ModificarReserva(UpdateReservaDTO reserva);
    ReservaDTO? ObtenerReservaPorId(int id);
    void EliminarReserva(int idReserva);
    // Consultas
    IEnumerable<ReservaDTO> ListarReservas(EstadoReserva? estado = null, Cliente? cliente = null, Tipohabitacion? tipo = null, int? numeroHabitacion = null);

    // Operaciones de Negocio
    void ConfirmarReserva(UpdateReservaDTO reserva);
    void CancelarReserva(UpdateReservaDTO reserva);

}