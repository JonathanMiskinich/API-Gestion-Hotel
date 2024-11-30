using HotelManagement.Models;

namespace Services.Clientes
{
    public class ClienteService
    {
        private HotelContext context;

        public ClienteService(HotelContext context)
        {
            this.context = context;
        }

        public void RegistrarCliente(Cliente cliente)
        {
            if (cliente != null)
                context.Clientes.Add(cliente);
            context.SaveChanges();
        }

        public Cliente? ObtenerClientePorID(int id)
        {
            return context.Clientes.FirstOrDefault(c => c.Id == id && !c.isDeleted);
        }

        public Cliente ObtenerClientePorApellido(string apellido)
        {
            if(string.IsNullOrEmpty(apellido))
                throw new Exception("Apellido nulo o vacio es invalido.");
            return  context.Clientes.FirstOrDefault(c => c.APELLIDO == apellido && !c.isDeleted);
        }

        public Cliente? ObtenerClientePorDNI(int dni)
        {
            return context.Clientes.FirstOrDefault(c => c.DNI == dni && !c.isDeleted);
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            return context.Clientes.Where(c => !c.isDeleted).ToList();
        } 
    }
}