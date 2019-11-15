using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamenFinal.Data.Migrations
{
    public partial class Factorador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "DetalleFactura",
                newName: "Total");

            migrationBuilder.AddColumn<string>(
                name: "nombreProducto",
                table: "Producto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombreProducto",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "DetalleFactura",
                newName: "SubTotal");
        }
    }
}
