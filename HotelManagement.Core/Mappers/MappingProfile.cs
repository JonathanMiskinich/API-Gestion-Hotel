using AutoMapper;
using HotelManagement.Core.DTO;
using HotelManagement.Core.Models;

namespace HotelManagement.Core.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Cliente
        CreateMap<Cliente, ClienteDTO>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NOMBRE))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.APELLIDO))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMAIL))
            .ForMember(dest => dest.Dni, opt => opt.MapFrom(src => src.DNI));

        CreateMap<CreateClienteDTO, Cliente>()
            .ForMember(dest => dest.NOMBRE, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.APELLIDO, opt => opt.MapFrom(src => src.Apellido))
            .ForMember(dest => dest.EMAIL, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.DNI, opt => opt.MapFrom(src => src.Dni));

        CreateMap<UpdateClienteDTO, Cliente>()
            .ForMember(dest => dest.NOMBRE, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.APELLIDO, opt => opt.MapFrom(src => src.Apellido))
            .ForMember(dest => dest.EMAIL, opt => opt.MapFrom(src => src.Email));

        // Reserva
        CreateMap<Reserva, ReservaDTO>()
            .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.FECHA_INICIO.ToDateTime(TimeOnly.MinValue)))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => src.FECHA_FINALIZACION.ToDateTime(TimeOnly.MinValue)));
        
        CreateMap<CreateReservaDTO, Reserva>()
            .ForMember(dest => dest.FECHA_INICIO, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaInicio)))
            .ForMember(dest => dest.FECHA_FINALIZACION, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaFin)));
            
        CreateMap<UpdateReservaDTO, Reserva>()
            .ForMember(dest => dest.FECHA_INICIO, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaInicio)))
            .ForMember(dest => dest.FECHA_FINALIZACION, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.FechaFin)));

        // Factura
        CreateMap<Factura, FacturaDTO>()
            .ForMember(dest => dest.MontoTotal, opt => opt.MapFrom(src => src.MONTO_TOTAL));
            
        CreateMap<CreateFacturaDTO, Factura>()
            .ForMember(dest => dest.MONTO_TOTAL, opt => opt.MapFrom(src => src.MontoTotal))
            .ForMember(dest => dest.FechaEmision, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)));
        
        CreateMap<UpdateFacturaDTO, Factura>()
            .ForMember(dest => dest.MONTO_TOTAL, opt => opt.MapFrom(src => src.MontoTotal));

        // Habitacion
        CreateMap<Habitacione, HabitacionDTO>()
            .ForMember(dest => dest.PrecioPorNoche, opt => opt.MapFrom(src => src.PRECIO_POR_NOCHE));

        CreateMap<CreateHabitacionDTO, Habitacione>()
            .ForMember(dest => dest.PRECIO_POR_NOCHE, opt => opt.MapFrom(src => src.PrecioPorNoche));

        CreateMap<UpdateHabitacionDTO, Habitacione>()
            .ForMember(dest => dest.PRECIO_POR_NOCHE, opt => opt.MapFrom(src => src.PrecioPorNoche));


    }
}
