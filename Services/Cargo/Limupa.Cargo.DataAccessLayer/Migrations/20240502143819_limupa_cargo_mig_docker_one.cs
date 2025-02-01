using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limupa.Cargo.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class limupa_cargo_mig_docker_one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoCompanies",
                columns: table => new
                {
                    CargoCompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCompanies", x => x.CargoCompanyID);
                });

            migrationBuilder.CreateTable(
                name: "CargoCustomers",
                columns: table => new
                {
                    CargoCustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoCustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoCustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCustomers", x => x.CargoCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "CargoOperations",
                columns: table => new
                {
                    CargoOperationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoBarcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoOperationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoOperations", x => x.CargoOperationID);
                });

            migrationBuilder.CreateTable(
                name: "CargoDetails",
                columns: table => new
                {
                    CargoDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoSenderCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoReceiverCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CargoBarcode = table.Column<int>(type: "int", nullable: false),
                    CargoCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoDetails", x => x.CargoDetailID);
                    table.ForeignKey(
                        name: "FK_CargoDetails_CargoCompanies_CargoCompanyId",
                        column: x => x.CargoCompanyId,
                        principalTable: "CargoCompanies",
                        principalColumn: "CargoCompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoDetails_CargoCompanyId",
                table: "CargoDetails",
                column: "CargoCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoCustomers");

            migrationBuilder.DropTable(
                name: "CargoDetails");

            migrationBuilder.DropTable(
                name: "CargoOperations");

            migrationBuilder.DropTable(
                name: "CargoCompanies");
        }
    }
}
