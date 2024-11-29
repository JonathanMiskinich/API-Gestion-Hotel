using HotelManagement.Models;

namespace Services.Cliente
{
    public class ClienteService
    {
        private HotelContext context;

        public ClienteService(HotelContext context)
        {
            this.context = context;
        }

        public static void RegistrarCliente(Cliente cliente)
        {
            if (cliente != null)
                context.Clientes.Add(cliente);
            context.Save();
        }
    }
}