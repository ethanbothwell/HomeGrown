using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeGrown.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsFarmer = table.Column<bool>(type: "INTEGER", nullable: false),
                    FarmId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            // Seed mock users with BCrypt-hashed passwords
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Name", "Email", "PasswordHash", "IsFarmer", "FarmId", "CreatedAt" },
                values: new object[,]
                {
                    { "Alex Johnson", "alex@example.com", "$2a$11$fWERz1XNcl9.dz2InJfKGuUWIWV.MZxfnPs.Kxhx6tEwSl1TwOUD2", false, null, "2026-01-01T00:00:00" },
                    { "Sarah Chen", "sarah@example.com", "$2a$11$fWERz1XNcl9.dz2InJfKGuUWIWV.MZxfnPs.Kxhx6tEwSl1TwOUD2", false, null, "2026-01-01T00:00:00" },
                    { "Mike Davis", "mike@example.com", "$2a$11$fWERz1XNcl9.dz2InJfKGuUWIWV.MZxfnPs.Kxhx6tEwSl1TwOUD2", false, null, "2026-01-01T00:00:00" },
                    { "Maria Thornton", "maria@sunridge.com", "$2a$11$vPu8BUUa/Y/VOaDzdi191.7Og/AkMRiUeIPhw7oS6T35uShPn5sqm", true, 1, "2026-01-01T00:00:00" },
                    { "James Whitfield", "james@bluehen.com", "$2a$11$5y0jPhxaEwp7t58Kk4RhY.8l6uVHOprKi.tN9HrH5CcBRtqmsMpf2", true, 2, "2026-01-01T00:00:00" },
                    { "Sofia Reyes", "sofia@kneadedloaf.com", "$2a$11$PTaJpj1SLsSrZcovNp2T6.1118I6yJH5FxB9hdWsuGp.3ZbZSDYEu", true, 3, "2026-01-01T00:00:00" },
                    { "Earl Mason", "earl@wildcreek.com", "$2a$11$ebkNzzTzTee0mYbdjKDVbuopYsWPJ4bgSXt1Vo4IabP55YV9F6yU.", true, 4, "2026-01-01T00:00:00" },
                    { "Greg Halverson", "greg@mossyoak.com", "$2a$11$7Yk4Rbg5iVfEvPNsOz/nWOUwSGrdxUqboCZuiaESmv4glLIdWLvZ6", true, 5, "2026-01-01T00:00:00" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
