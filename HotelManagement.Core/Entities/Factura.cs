﻿using System;
using System.Collections.Generic;

namespace HotelManagement.Core.Models;

public partial class Factura
{
    public int Id { get; private set; }

    public int IdCliente { get; private set; }

    public int IdReserva { get;  private set; }

    private decimal MontoTotal;

    public DateOnly FechaEmision { get; private set; }

    public virtual Cliente ClienteNavigation { get; private set; } = null!;

    public virtual Reserva ReservaNavigation { get; private set; } = null!;

    public Factura(){}

    public Factura(int IdCliente, int IdReserva, decimal MontoTotal, Cliente cliente, Reserva reserva) 
    {
        this.IdCliente = IdCliente;
        this.IdReserva = IdReserva;
        this.MONTO_TOTAL = MontoTotal;
        this.ClienteNavigation = cliente;
        this.FechaEmision = DateOnly.FromDateTime(DateTime.Now);
        this.ReservaNavigation = reserva;
    }
    public decimal MONTO_TOTAL
    {
        get => this.MontoTotal;
        set 
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException("Monto ingresado invalido.");
            this.MontoTotal = value;
        }
    }

}
