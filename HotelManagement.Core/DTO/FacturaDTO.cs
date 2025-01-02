namespace HotelManagement.Core.DTO;

// DTO para consultas de informacion de facturas
public class FacturaDTO
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public int IdReserva { get; set; }
    public decimal MontoTotal { get; set; }
    public DateOnly FechaEmision { get; set; }
}

// DTO para crear una factura
public class CreateFacturaDTO
{
    public int IdCliente { get; set; }
    public int IdReserva { get; set; }
    public decimal MontoTotal { get; set; }
}

// DTO para actualizar una factura
public class UpdateFacturaDTO
{
    public int Id { get; set; }
    public decimal MontoTotal { get; set; }
}
