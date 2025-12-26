using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskOrderManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Tasks");
        }
    }
}
