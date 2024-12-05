﻿// <auto-generated />
using System;
using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelManagement.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20241205233442_ModificacionAtributosNulos")]
    partial class ModificacionAtributosNulos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("HotelManagement.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("APELLIDO")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellido");

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<int>("Dni")
                        .HasColumnType("int")
                        .HasColumnName("dni");

                    b.Property<string>("EMAIL")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<DateTime>("FechaEliminacion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("FechaEliminacion");

                    b.Property<string>("NOMBRE")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("telefono");

                    b.Property<bool>("isDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("isDeleted");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("clientes", (string)null);
                });

            modelBuilder.Entity("HotelManagement.Models.EstadoReserva", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("descripcion");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("estado_reserva", (string)null);
                });

            modelBuilder.Entity("HotelManagement.Models.Estadohabitacion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("descripcion");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("estadohabitacion", (string)null);
                });

            modelBuilder.Entity("HotelManagement.Models.Factura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("FechaEmision")
                        .HasColumnType("date")
                        .HasColumnName("fechaEmision");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("idCliente");

                    b.Property<int>("IdReserva")
                        .HasColumnType("int")
                        .HasColumnName("idReserva");

                    b.Property<decimal>("MONTO_TOTAL")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCliente" }, "idCliente");

                    b.HasIndex(new[] { "IdReserva" }, "idReserva");

                    b.ToTable("factura", (string)null);
                });

            modelBuilder.Entity("HotelManagement.Models.Habitacione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<int>("Numero")
                        .HasColumnType("int")
                        .HasColumnName("numero");

                    b.Property<decimal>("PRECIO_POR_NOCHE")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("precioPorNoche");

                    b.Property<int?>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Estado" }, "estado");

                    b.HasIndex(new[] { "Numero" }, "numero")
                        .IsUnique();

                    b.HasIndex(new[] { "Tipo" }, "tipo");

                    b.ToTable("habitaciones", (string)null);
                });

            modelBuilder.Entity("HotelManagement.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("FECHA_FINALIZACION")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FECHA_INICIO")
                        .HasColumnType("date");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int")
                        .HasColumnName("idCliente");

                    b.Property<int?>("IdEstadoReserva")
                        .HasColumnType("int")
                        .HasColumnName("idEstadoReserva");

                    b.Property<int>("IdHabitacion")
                        .HasColumnType("int")
                        .HasColumnName("idHabitacion");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCliente" }, "idCliente")
                        .HasDatabaseName("idCliente1");

                    b.HasIndex(new[] { "IdEstadoReserva" }, "idEstadoReserva");

                    b.HasIndex(new[] { "IdHabitacion" }, "idHabitacion");

                    b.ToTable("reservas", (string)null);
                });

            modelBuilder.Entity("HotelManagement.Models.Tipohabitacion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("descripcion");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("tipohabitacion", (string)null);
                });

            modelBuilder.Entity("HotelManagement.Models.Factura", b =>
                {
                    b.HasOne("HotelManagement.Models.Cliente", "ClienteNavigation")
                        .WithMany("Facturas")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("factura_ibfk_1");

                    b.HasOne("HotelManagement.Models.Reserva", "ReservaNavigation")
                        .WithMany("Facturas")
                        .HasForeignKey("IdReserva")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("factura_ibfk_2");

                    b.Navigation("ClienteNavigation");

                    b.Navigation("ReservaNavigation");
                });

            modelBuilder.Entity("HotelManagement.Models.Habitacione", b =>
                {
                    b.HasOne("HotelManagement.Models.Estadohabitacion", "EstadoNavigation")
                        .WithMany("Habitaciones")
                        .HasForeignKey("Estado")
                        .HasConstraintName("habitaciones_ibfk_2");

                    b.HasOne("HotelManagement.Models.Tipohabitacion", "TipoNavigation")
                        .WithMany("Habitaciones")
                        .HasForeignKey("Tipo")
                        .HasConstraintName("habitaciones_ibfk_1");

                    b.Navigation("EstadoNavigation");

                    b.Navigation("TipoNavigation");
                });

            modelBuilder.Entity("HotelManagement.Models.Reserva", b =>
                {
                    b.HasOne("HotelManagement.Models.Cliente", "ClienteNavigation")
                        .WithMany("Reservas")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("reservas_ibfk_1");

                    b.HasOne("HotelManagement.Models.EstadoReserva", "EstadoReservaNavigation")
                        .WithMany("Reservas")
                        .HasForeignKey("IdEstadoReserva")
                        .HasConstraintName("reservas_ibfk_3");

                    b.HasOne("HotelManagement.Models.Habitacione", "HabitacionNavigation")
                        .WithMany("Reservas")
                        .HasForeignKey("IdHabitacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("reservas_ibfk_2");

                    b.Navigation("ClienteNavigation");

                    b.Navigation("EstadoReservaNavigation");

                    b.Navigation("HabitacionNavigation");
                });

            modelBuilder.Entity("HotelManagement.Models.Cliente", b =>
                {
                    b.Navigation("Facturas");

                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("HotelManagement.Models.EstadoReserva", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("HotelManagement.Models.Estadohabitacion", b =>
                {
                    b.Navigation("Habitaciones");
                });

            modelBuilder.Entity("HotelManagement.Models.Habitacione", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("HotelManagement.Models.Reserva", b =>
                {
                    b.Navigation("Facturas");
                });

            modelBuilder.Entity("HotelManagement.Models.Tipohabitacion", b =>
                {
                    b.Navigation("Habitaciones");
                });
#pragma warning restore 612, 618
        }
    }
}