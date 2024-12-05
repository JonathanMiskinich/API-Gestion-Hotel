using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionAtributosNulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "factura_ibfk_1",
                table: "factura");

            migrationBuilder.DropForeignKey(
                name: "factura_ibfk_2",
                table: "factura");

            migrationBuilder.DropForeignKey(
                name: "reservas_ibfk_1",
                table: "reservas");

            migrationBuilder.DropForeignKey(
                name: "reservas_ibfk_2",
                table: "reservas");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "reservas",
                newName: "FECHA_INICIO");

            migrationBuilder.RenameColumn(
                name: "FechaFinalizacion",
                table: "reservas",
                newName: "FECHA_FINALIZACION");

            migrationBuilder.AlterColumn<int>(
                name: "idHabitacion",
                table: "reservas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idCliente",
                table: "reservas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "numero",
                table: "habitaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idReserva",
                table: "factura",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idCliente",
                table: "factura",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "factura_ibfk_1",
                table: "factura",
                column: "idCliente",
                principalTable: "clientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "factura_ibfk_2",
                table: "factura",
                column: "idReserva",
                principalTable: "reservas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "reservas_ibfk_1",
                table: "reservas",
                column: "idCliente",
                principalTable: "clientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "reservas_ibfk_2",
                table: "reservas",
                column: "idHabitacion",
                principalTable: "habitaciones",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "factura_ibfk_1",
                table: "factura");

            migrationBuilder.DropForeignKey(
                name: "factura_ibfk_2",
                table: "factura");

            migrationBuilder.DropForeignKey(
                name: "reservas_ibfk_1",
                table: "reservas");

            migrationBuilder.DropForeignKey(
                name: "reservas_ibfk_2",
                table: "reservas");

            migrationBuilder.RenameColumn(
                name: "FECHA_INICIO",
                table: "reservas",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "FECHA_FINALIZACION",
                table: "reservas",
                newName: "FechaFinalizacion");

            migrationBuilder.AlterColumn<int>(
                name: "idHabitacion",
                table: "reservas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idCliente",
                table: "reservas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "numero",
                table: "habitaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idReserva",
                table: "factura",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idCliente",
                table: "factura",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "factura_ibfk_1",
                table: "factura",
                column: "idCliente",
                principalTable: "clientes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "factura_ibfk_2",
                table: "factura",
                column: "idReserva",
                principalTable: "reservas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "reservas_ibfk_1",
                table: "reservas",
                column: "idCliente",
                principalTable: "clientes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "reservas_ibfk_2",
                table: "reservas",
                column: "idHabitacion",
                principalTable: "habitaciones",
                principalColumn: "id");
        }
    }
}
