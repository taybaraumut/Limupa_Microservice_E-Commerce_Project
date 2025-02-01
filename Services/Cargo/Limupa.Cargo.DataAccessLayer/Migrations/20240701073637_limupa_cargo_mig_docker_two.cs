using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limupa.Cargo.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class limupa_cargo_mig_docker_two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CargoCustomerUserID",
                table: "CargoCustomers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoCustomerUserID",
                table: "CargoCustomers");
        }
    }
}
