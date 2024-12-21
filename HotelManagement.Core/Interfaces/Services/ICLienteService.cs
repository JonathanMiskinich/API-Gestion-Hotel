using HotelManagement.Core.Models;

namespace HotelManagement.Core.Interfaces.Services;

public interface IClienteService
{
    List<Cliente> ObtenerClientesActivos();
    Cliente? ObtenerClientePorId(int id);
    Cliente CrearCliente(string nombre, string apellido, string telefono, string email, int dni);
    void ActualizarCliente(Cliente cliente);
    void EliminarCliente(int id);
    void RegistrarCliente(Cliente cliente);
}