using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_UserNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserNames",
                schema: "dbo",
                table: "Notification",
                newName: "UserIds");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "event",
                table: "EventUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatorUserName",
                schema: "event",
                table: "EventUser",
                newName: "CreatorUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserIds",
                schema: "dbo",
                table: "Notification",
                newName: "UserNames");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "event",
                table: "EventUser",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                schema: "event",
                table: "EventUser",
                newName: "CreatorUserName");
        }
    }
}
