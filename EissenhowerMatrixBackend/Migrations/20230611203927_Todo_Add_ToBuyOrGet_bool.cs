using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EissenhowerMatrixBackend.Migrations
{
    /// <inheritdoc />
    public partial class Todo_Add_ToBuyOrGet_bool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ToBuyOrGet",
                table: "Todos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToBuyOrGet",
                table: "Todos");
        }
    }
}
