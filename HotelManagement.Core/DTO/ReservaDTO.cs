namespace HotelManagement.Core.DTO;

// DTO para consultas de informacion de reservas
public class ReservaDTO
{
    public int Id { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
    public int IdCliente { get; set; }
    public int IdHabitacion { get; set; }
    public int IdEstadoReserva { get; set; }
}

public class CreateReservaDTO
{
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
    public int IdCliente { get; set; }
    public int IdHabitacion { get; set; }
    public int IdEstadoReserva { get; set; }
}

public class UpdateReservaDTO
{
    public int Id { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set; }
    public int IdHabitacion { get; set; }
    public int IdEstadoReserva { get; set; }
}
