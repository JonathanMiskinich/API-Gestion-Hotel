using System;
using System.Collections.Generic;
using HotelManagement.Helpers;

namespace HotelManagement.Models;

public partial class Reserva
{
    public int Id { get; private set; }

    public int IdCliente { get; private set; }

    public int IdHabitacion { get; private set; }

    private DateOnly FechaInicio;

    private DateOnly FechaFinalizacion;
    
    public int? IdEstadoReserva { get; set; }

    public virtual ICollection<Factura> Facturas { get; private set; } = new List<Factura>();

    public virtual Cliente ClienteNavigation { get; private set; } = null!;

    public virtual EstadoReserva EstadoReservaNavigation { get; set; }  = null!;

    public virtual Habitacione HabitacionNavigation { get; private set; } = null!;

    public Reserva(){}

    public Reserva(int IdCliente, int IdHabitacion, DateOnly Inicio, DateOnly Final, int IdEstadoReserva, Cliente cliente, Habitacione habitacion, EstadoReserva estado)
    {
        this.IdCliente = IdCliente;
        this.IdHabitacion = IdHabitacion;
        FechaInicio = Inicio;
        FechaFinalizacion = Final;
        this.IdEstadoReserva = IdEstadoReserva;
        this.ClienteNavigation = cliente;
        this.EstadoReservaNavigation = estado;
        HabitacionNavigation = habitacion;
    }

    public void AgregarFactura(Factura factura)
    {
        this.Facturas.Add(factura);
    }

    public void EliminarFactura(Factura factura)
    {
        if(Facturas.Contains(factura))
            this.Facturas.Remove(factura);
    }

    public DateOnly FECHA_INICIO
    {
        get => this.FechaInicio;
        set
        {
            if(value > FechaFinalizacion)
                throw new InvalidOperationException("La fecha de inicio no puede ser despues de la fecha de finalizacion.");
            this.FechaInicio = value;
        }
    }

    public DateOnly FECHA_FINALIZACION
    {
        get => this.FechaFinalizacion;
        set
        {
            if(value < FechaInicio)
                throw new InvalidOperationException("La fecha de finalizacion no puede ser menor a la de inicio");
            Validaciones.ValidarNoNulo(value, "Fecha inicio");
            this.FechaFinalizacion = value;
        }
    }
}
