using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEventUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventUser_Events_EventId",
                schema: "event",
                table: "eventUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_eventUser",
                schema: "event",
                table: "eventUser");

            migrationBuilder.RenameTable(
                name: "eventUser",
                schema: "event",
                newName: "EventUser",
                newSchema: "event");

            migrationBuilder.RenameIndex(
                name: "IX_eventUser_EventId",
                schema: "event",
                table: "EventUser",
                newName: "IX_EventUser_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventUser",
                schema: "event",
                table: "EventUser",
                columns: new[] { "EventId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Events_EventId",
                schema: "event",
                table: "EventUser",
                column: "EventId",
                principalSchema: "event",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Events_EventId",
                schema: "event",
                table: "EventUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventUser",
                schema: "event",
                table: "EventUser");

            migrationBuilder.RenameTable(
                name: "EventUser",
                schema: "event",
                newName: "eventUser",
                newSchema: "event");

            migrationBuilder.RenameIndex(
                name: "IX_EventUser_EventId",
                schema: "event",
                table: "eventUser",
                newName: "IX_eventUser_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_eventUser",
                schema: "event",
                table: "eventUser",
                columns: new[] { "EventId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_eventUser_Events_EventId",
                schema: "event",
                table: "eventUser",
                column: "EventId",
                principalSchema: "event",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
