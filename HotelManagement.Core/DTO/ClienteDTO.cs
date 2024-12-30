namespace HotelManagement.Core.DTO;

// DTO para consultas de informacion de clientes
public class ClienteDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string? Email { get; set; }
    public int Dni { get; set; }
    public bool isDeleted { get; set; }
}

// DTO para crear un cliente
public class CreateClienteDTO
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string? Email { get; set; }
    public int Dni { get; set; }
}

// DTO para actualizar un cliente
public class UpdateClienteDTO
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string? Email { get; set; }
}
