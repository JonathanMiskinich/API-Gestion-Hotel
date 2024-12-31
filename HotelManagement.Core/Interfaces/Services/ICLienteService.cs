using HotelManagement.Core.DTO;
using HotelManagement.Core.Models;

namespace HotelManagement.Core.Interfaces.Services;

public interface IClienteService
{
    List<ClienteDTO> ObtenerClientesActivos();
    ClienteDTO? ObtenerClientePorId(int id);
    Cliente CrearCliente(CreateClienteDTO cliente);
    void ActualizarCliente(UpdateClienteDTO cliente);
    void EliminarCliente(int id);
    void RegistrarCliente(CreateClienteDTO cliente);
}