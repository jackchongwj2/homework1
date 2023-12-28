using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace homework1.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Marks",
                table: "Results",
                type: "numeric(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Marks",
                table: "Results",
                type: "numeric(4,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,1)",
                oldNullable: true);
        }
    }
}
