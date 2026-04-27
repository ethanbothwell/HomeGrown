using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeGrown.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCommunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Community",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Community",
                table: "Users");
        }
    }
}
