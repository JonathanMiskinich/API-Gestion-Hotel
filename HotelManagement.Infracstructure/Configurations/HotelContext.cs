﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HotelManagement.Core.Models;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<EstadoReserva> EstadoReservas { get; set; }

    public virtual DbSet<Estadohabitacion> Estadohabitacions { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Tipohabitacion> Tipohabitacions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;database=hotel;user=root;password=1234", 
                new MySqlServerVersion(new Version(8, 0, 30))); 
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.APELLIDO)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.DNI).HasColumnName("dni");
            entity.Property(e => e.EMAIL)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.NOMBRE)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");

             // Mapear propiedades protegidas y backing fields
            entity.Property(e => e.isDeleted)
                .HasColumnName("isDeleted")
                .HasDefaultValue(false);

            entity.Property(e => e.FechaEliminacion)
                .HasColumnName("FechaEliminacion")
                .HasConversion(
                    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime)
                );
        });

        modelBuilder.Entity<EstadoReserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estado_reserva");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Estadohabitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadohabitacion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("factura");

            entity.HasIndex(e => e.IdCliente, "idCliente");

            entity.HasIndex(e => e.IdReserva, "idReserva");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaEmision).HasColumnName("fechaEmision");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.MONTO_TOTAL)
                .HasPrecision(10, 2)
                .HasColumnName("montoTotal");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("factura_ibfk_1");

            entity.HasOne(d => d.ReservaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("factura_ibfk_2");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("habitaciones");

            entity.HasIndex(e => e.Estado, "estado");

            entity.HasIndex(e => e.Numero, "numero").IsUnique();

            entity.HasIndex(e => e.Tipo, "tipo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.PRECIO_POR_NOCHE)
                .HasPrecision(10, 2)
                .HasColumnName("precioPorNoche");
            entity.Property(e => e.Tipo).HasColumnName("tipo");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("habitaciones_ibfk_2");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.Tipo)
                .HasConstraintName("habitaciones_ibfk_1");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reservas");

            entity.HasIndex(e => e.IdCliente, "idCliente");

            entity.HasIndex(e => e.IdEstadoReserva, "idEstadoReserva");

            entity.HasIndex(e => e.IdHabitacion, "idHabitacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdEstadoReserva).HasColumnName("idEstadoReserva");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("reservas_ibfk_1");

            entity.HasOne(d => d.EstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .HasConstraintName("reservas_ibfk_3");

            entity.HasOne(d => d.HabitacionNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("reservas_ibfk_2");
        });

        modelBuilder.Entity<Tipohabitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipohabitacion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
