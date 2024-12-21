using HotelManagement.Core.Models;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;

namespace HotelManagement.Infracstructure.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HotelContext context;

        public ClienteService(HotelContext context)
        {
            this.context = context;
        }

        public void RegistrarCliente(Cliente cliente)
        {
            Validaciones.ValidarNoNulo(cliente, "cliente");

            if (ClienteExiste(cliente.DNI))
                throw new Exception("El cliente ya se encuentra registrado.");

            context.Clientes.Add(cliente);
        }

        public Cliente? ObtenerClientePorId(int id)
        {
            Validaciones.ValidarValorPositivo(id, "ID");

            return context.Clientes.FirstOrDefault(c => c.Id == id && !c.isDeleted);
        }

        public Cliente? ObtenerClientePorApellido(string apellido)
        {
            Validaciones.ValidarTextoNoVacio(apellido, "apellido");

            return  context.Clientes.FirstOrDefault(c => c.APELLIDO == apellido && !c.isDeleted);
        }

        public Cliente? ObtenerClientePorDNI(int dni)
        {
            Validaciones.ValidarValorPositivo(dni, "DNI");

            return context.Clientes.FirstOrDefault(c => c.DNI == dni && !c.isDeleted);
        }

        public List<Cliente> ObtenerClientesActivos()
        {
            return context.Clientes.Where(c => !c.isDeleted).ToList();
        }

        public void EliminarCliente(int id)
        {
            Validaciones.ValidarValorPositivo(id, "ID");

            var cliente = ObtenerClientePorId(id);
            if (cliente == null)
                throw new KeyNotFoundException("No se encontró un cliente con el ID especificado.");

            cliente.Eliminar();
        }

        public void ActualizarCliente(Cliente cliente)
        {
            Validaciones.ValidarNoNulo(cliente, "cliente");

            context.Clientes.Update(cliente);
        }

        public Cliente CrearCliente(string nombre, string apellido, string telefono, string email, int dni)
        {
            var cliente = new Cliente(nombre, apellido, telefono, email, dni);
            return cliente;
        }

        private bool ClienteExiste(int dni)
        {
            return context.Clientes.Any(c => c.DNI == dni);
        }

    }
}