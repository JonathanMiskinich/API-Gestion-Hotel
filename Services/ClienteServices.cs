using HotelManagement.Models;
using HotelManagement.Helpers;

namespace HotelManagement.Services
{
    public class ClienteService
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
                throw new Exception("El cliente ya se encuentra registadro.");

            context.Clientes.Add(cliente);
        }

        public Cliente? ObtenerClientePorID(int id)
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

        public IEnumerable<Cliente> ObtenerClientesActivos()
        {
            return context.Clientes.Where(c => !c.isDeleted).ToList();
        }

        public void EliminarClienteLogicamente(int id)
        {
            Validaciones.ValidarValorPositivo(id, "ID");

            var cliente = ObtenerClientePorID(id);
            if (cliente == null)
                throw new KeyNotFoundException("No se encontró un cliente con el ID especificado.");

            cliente.Eliminar();
        }

        private bool ClienteExiste(int dni)
        {
            return context.Clientes.Any(c => c.DNI == dni);
        }
    }
}