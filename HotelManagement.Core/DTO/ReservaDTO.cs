namespace HotelManagement.Core.DTO;

// DTO para consultas de informacion de reservas
public class ReservaDTO
{
    public int Id { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int IdCliente { get; set; }
    public int IdHabitacion { get; set; }
    public int IdEstadoReserva { get; set; }
}

public class CreateReservaDTO
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int IdCliente { get; set; }
    public int IdHabitacion { get; set; }
    public int IdEstadoReserva { get; set; }
}

public class UpdateReservaDTO
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int IdHabitacion { get; set; }
    public int IdEstadoReserva { get; set; }
}
