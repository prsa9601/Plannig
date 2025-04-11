using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowedInstagramPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedPostInstagram",
                schema: "user",
                table: "userPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllowedPostTelegram",
                schema: "user",
                table: "userPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllowedStoryInstagram",
                schema: "user",
                table: "userPackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllowedPostInstagram",
                schema: "package",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllowedPostTelegram",
                schema: "package",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllowedStoryInstagram",
                schema: "package",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedPostInstagram",
                schema: "user",
                table: "userPackages");

            migrationBuilder.DropColumn(
                name: "AllowedPostTelegram",
                schema: "user",
                table: "userPackages");

            migrationBuilder.DropColumn(
                name: "AllowedStoryInstagram",
                schema: "user",
                table: "userPackages");

            migrationBuilder.DropColumn(
                name: "AllowedPostInstagram",
                schema: "package",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AllowedPostTelegram",
                schema: "package",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AllowedStoryInstagram",
                schema: "package",
                table: "Package");
        }
    }
}
