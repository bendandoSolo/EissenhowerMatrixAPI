using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EissenhowerMatrixBackend.Migrations
{
    /// <inheritdoc />
    public partial class EissenhowerStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Todos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Todos");
        }
    }
}
