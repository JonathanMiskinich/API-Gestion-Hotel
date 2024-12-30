namespace HotelManagement.Core.DTO;

// DTO para consultas de informacion de habitaciones
public class HabitacionDTO
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public int Tipo { get; set; }
    public decimal PrecioPorNoche { get; set; }
    public int Estado { get; set; }
}

// DTO para crear una habitacion
public class CreateHabitacionDTO
{
    public int Numero { get; set; }
    public int Tipo { get; set; }
    public decimal PrecioPorNoche { get; set; }
    public int Estado { get; set; }
}

// DTO para actualizar una habitacion
public class UpdateHabitacionDTO
{
    public int Numero { get; set; }
    public int? Tipo { get; set; }
    public decimal PrecioPorNoche { get; set; }
    public int Estado { get; set; }
}
