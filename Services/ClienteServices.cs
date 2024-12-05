using HotelManagement.Models;

namespace Services.Clientes
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
            if (cliente == null)
                throw new ArgumentNullException("No se permite cliente nulos");
            if (context.Clientes.Any(c => c.DNI == cliente.DNI))
                throw new Exception("El cliente ya se encuentra registadro.");

            context.Clientes.Add(cliente);
        }

        public Cliente? ObtenerClientePorID(int id)
        {
            if(id <  0)
                throw new ArgumentOutOfRangeException("No se permite un ID negativo.");
            return context.Clientes.FirstOrDefault(c => c.Id == id && !c.isDeleted);
        }

        public Cliente? ObtenerClientePorApellido(string apellido)
        {
            if(string.IsNullOrEmpty(apellido))
                throw new ArgumentNullException("Apellido nulo o vacio es invalido.");
            return  context.Clientes.FirstOrDefault(c => c.APELLIDO == apellido && !c.isDeleted);
        }

        public Cliente? ObtenerClientePorDNI(int dni)
        {
            if(dni <  0)
                throw new ArgumentOutOfRangeException("No se permite un DNI negativo");
            return context.Clientes.FirstOrDefault(c => c.DNI == dni && !c.isDeleted);
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            return context.Clientes.Where(c => !c.isDeleted).ToList();
        } 
    }
}