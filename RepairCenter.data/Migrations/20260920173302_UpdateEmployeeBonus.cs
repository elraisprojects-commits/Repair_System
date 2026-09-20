using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairCenter.data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeBonus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "EmployeeBonuses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "EmployeeBonuses");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "EmployeeBonuses",
                newName: "DeductionAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "BonusAmount",
                table: "EmployeeBonuses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusAmount",
                table: "EmployeeBonuses");

            migrationBuilder.RenameColumn(
                name: "DeductionAmount",
                table: "EmployeeBonuses",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "EmployeeBonuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "EmployeeBonuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
