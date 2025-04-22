using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertiesInPostInstagramPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostInstagramId1_PostId1",
                schema: "instagram",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Posts_PostInstagramId1_PostId1",
                schema: "instagram",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                schema: "instagram",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                schema: "instagram",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PostId1",
                schema: "instagram",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PostId1",
                schema: "instagram",
                table: "Images");

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                schema: "instagram",
                table: "Videos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "instagram",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                schema: "instagram",
                table: "Images",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "instagram",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                schema: "instagram",
                table: "Videos",
                columns: new[] { "PostInstagramId1", "PostId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                schema: "instagram",
                table: "Images",
                columns: new[] { "PostInstagramId1", "PostId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostInstagramId1_PostId",
                schema: "instagram",
                table: "Images",
                columns: new[] { "PostInstagramId1", "PostId" },
                principalSchema: "instagram",
                principalTable: "Posts",
                principalColumns: new[] { "InstagramId1", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Posts_PostInstagramId1_PostId",
                schema: "instagram",
                table: "Videos",
                columns: new[] { "PostInstagramId1", "PostId" },
                principalSchema: "instagram",
                principalTable: "Posts",
                principalColumns: new[] { "InstagramId1", "Id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostInstagramId1_PostId",
                schema: "instagram",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Posts_PostInstagramId1_PostId",
                schema: "instagram",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                schema: "instagram",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                schema: "instagram",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "instagram",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                schema: "instagram",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PostId1",
                schema: "instagram",
                table: "Videos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "instagram",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                schema: "instagram",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PostId1",
                schema: "instagram",
                table: "Images",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                schema: "instagram",
                table: "Videos",
                columns: new[] { "PostInstagramId1", "PostId1", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                schema: "instagram",
                table: "Images",
                columns: new[] { "PostInstagramId1", "PostId1", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostInstagramId1_PostId1",
                schema: "instagram",
                table: "Images",
                columns: new[] { "PostInstagramId1", "PostId1" },
                principalSchema: "instagram",
                principalTable: "Posts",
                principalColumns: new[] { "InstagramId1", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Posts_PostInstagramId1_PostId1",
                schema: "instagram",
                table: "Videos",
                columns: new[] { "PostInstagramId1", "PostId1" },
                principalSchema: "instagram",
                principalTable: "Posts",
                principalColumns: new[] { "InstagramId1", "Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
