using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsEquipmentManagementProject.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityInPurchasePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchasePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchasePlans");
        }
    }
}
