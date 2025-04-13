using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTelegramAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "token",
                schema: "telegram",
                table: "Telegrams",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "chat_id",
                schema: "telegram",
                table: "Telegrams",
                newName: "Chat_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                schema: "telegram",
                table: "Telegrams",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "Chat_Id",
                schema: "telegram",
                table: "Telegrams",
                newName: "chat_id");
        }
    }
}
