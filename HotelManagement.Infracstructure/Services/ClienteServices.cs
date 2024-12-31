using HotelManagement.Core.Models;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces.Services;
using AutoMapper;
using HotelManagement.Core.DTO;


namespace HotelManagement.Infracstructure.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HotelContext context;
        private readonly IMapper mapper;

        public ClienteService(HotelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void RegistrarCliente(CreateClienteDTO clienteDTO)
        {
            Validaciones.ValidarNoNulo(clienteDTO, "cliente");

            if (ClienteExiste(clienteDTO.Dni))
                throw new Exception("El cliente ya se encuentra registrado.");

            context.Clientes.Add(CrearCliente(clienteDTO));
        }

        public ClienteDTO? ObtenerClientePorId(int id)
        {
            Validaciones.ValidarValorPositivo(id, "ID");

            var cliente = context.Clientes.FirstOrDefault(c => c.Id == id && !c.isDeleted);
            if (cliente == null)
                throw new KeyNotFoundException("No se encontró un cliente con el ID especificado.");

            return mapper.Map<ClienteDTO>(cliente);
        }

        public ClienteDTO? ObtenerClientePorApellido(string apellido)
        {
            Validaciones.ValidarTextoNoVacio(apellido, "apellido");
            var cliente = context.Clientes.FirstOrDefault(c => c.APELLIDO == apellido && !c.isDeleted);

            if (cliente == null)
                throw new KeyNotFoundException("No se encontró un cliente con el apellido especificado.");

            return mapper.Map<ClienteDTO>(cliente);
        }

        public ClienteDTO? ObtenerClientePorDNI(int dni)
        {
            Validaciones.ValidarValorPositivo(dni, "DNI");

            var cliente = context.Clientes.FirstOrDefault(c => c.DNI == dni && !c.isDeleted);
            if (cliente == null)
                throw new KeyNotFoundException("No se encontró un cliente con el DNI especificado.");

            return mapper.Map<ClienteDTO>(cliente);
        }

        public List<ClienteDTO> ObtenerClientesActivos()
        {
            return mapper.Map<List<ClienteDTO>>(context.Clientes.Where(c => !c.isDeleted).ToList());
        }

        public void EliminarCliente(int id)
        {
            Validaciones.ValidarValorPositivo(id, "ID");

            var cliente = context.Clientes.FirstOrDefault(c => c.Id == id && !c.isDeleted);
            if (cliente == null)
                throw new KeyNotFoundException("No se encontró un cliente con el ID especificado.");

            cliente.Eliminar();
        }

        public void ActualizarCliente(UpdateClienteDTO clienteDTO)
        {
            Validaciones.ValidarNoNulo(clienteDTO, "cliente");

            var cliente = mapper.Map<Cliente>(clienteDTO);

            context.Clientes.Update(cliente);
        }

        public Cliente CrearCliente(CreateClienteDTO clienteDTO)
        {
            return mapper.Map<Cliente>(clienteDTO);
        }

        private bool ClienteExiste(int dni)
        {
            return context.Clientes.Any(c => c.DNI == dni);
        }

    }
}