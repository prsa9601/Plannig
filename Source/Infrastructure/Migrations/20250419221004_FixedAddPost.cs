using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedAddPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostInstagramId_PostId1",
                schema: "instagram",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Instagram_InstagramId",
                schema: "instagram",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Instagram_InstagramId",
                schema: "instagram",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Posts_PostInstagramId_PostId1",
                schema: "instagram",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "InstagramProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stories",
                schema: "instagram",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                schema: "instagram",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageName",
                schema: "instagram",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VideoName",
                schema: "instagram",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SendMethod",
                schema: "instagram",
                table: "Instagram");

            migrationBuilder.RenameColumn(
                name: "PostInstagramId",
                schema: "instagram",
                table: "Videos",
                newName: "PostInstagramId1");

            migrationBuilder.RenameColumn(
                name: "Secuence",
                schema: "instagram",
                table: "Images",
                newName: "Seqence");

            migrationBuilder.RenameColumn(
                name: "PostInstagramId",
                schema: "instagram",
                table: "Images",
                newName: "PostInstagramId1");

            migrationBuilder.AlterColumn<string>(
                name: "InstagramId",
                schema: "instagram",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "InstagramId1",
                schema: "instagram",
                table: "Stories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "InstagramId",
                schema: "instagram",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "InstagramId1",
                schema: "instagram",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PageId",
                schema: "instagram",
                table: "Instagram",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profile",
                schema: "instagram",
                table: "Instagram",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "instagram",
                table: "Instagram",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stories",
                schema: "instagram",
                table: "Stories",
                columns: new[] { "InstagramId1", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                schema: "instagram",
                table: "Posts",
                columns: new[] { "InstagramId1", "Id" });

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
                name: "FK_Posts_Instagram_InstagramId1",
                schema: "instagram",
                table: "Posts",
                column: "InstagramId1",
                principalSchema: "instagram",
                principalTable: "Instagram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Instagram_InstagramId1",
                schema: "instagram",
                table: "Stories",
                column: "InstagramId1",
                principalSchema: "instagram",
                principalTable: "Instagram",
                principalColumn: "Id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostInstagramId1_PostId1",
                schema: "instagram",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Instagram_InstagramId1",
                schema: "instagram",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Instagram_InstagramId1",
                schema: "instagram",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Posts_PostInstagramId1_PostId1",
                schema: "instagram",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stories",
                schema: "instagram",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                schema: "instagram",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "InstagramId1",
                schema: "instagram",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "InstagramId1",
                schema: "instagram",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PageId",
                schema: "instagram",
                table: "Instagram");

            migrationBuilder.DropColumn(
                name: "Profile",
                schema: "instagram",
                table: "Instagram");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "instagram",
                table: "Instagram");

            migrationBuilder.RenameColumn(
                name: "PostInstagramId1",
                schema: "instagram",
                table: "Videos",
                newName: "PostInstagramId");

            migrationBuilder.RenameColumn(
                name: "Seqence",
                schema: "instagram",
                table: "Images",
                newName: "Secuence");

            migrationBuilder.RenameColumn(
                name: "PostInstagramId1",
                schema: "instagram",
                table: "Images",
                newName: "PostInstagramId");

            migrationBuilder.AlterColumn<long>(
                name: "InstagramId",
                schema: "instagram",
                table: "Stories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "InstagramId",
                schema: "instagram",
                table: "Posts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                schema: "instagram",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                schema: "instagram",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SendMethod",
                schema: "instagram",
                table: "Instagram",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stories",
                schema: "instagram",
                table: "Stories",
                columns: new[] { "InstagramId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                schema: "instagram",
                table: "Posts",
                columns: new[] { "InstagramId", "Id" });

            migrationBuilder.CreateTable(
                name: "InstagramProfile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstagramProfile_Instagram_InstagramId",
                        column: x => x.InstagramId,
                        principalSchema: "instagram",
                        principalTable: "Instagram",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstagramProfile_InstagramId",
                table: "InstagramProfile",
                column: "InstagramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostInstagramId_PostId1",
                schema: "instagram",
                table: "Images",
                columns: new[] { "PostInstagramId", "PostId1" },
                principalSchema: "instagram",
                principalTable: "Posts",
                principalColumns: new[] { "InstagramId", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Instagram_InstagramId",
                schema: "instagram",
                table: "Posts",
                column: "InstagramId",
                principalSchema: "instagram",
                principalTable: "Instagram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Instagram_InstagramId",
                schema: "instagram",
                table: "Stories",
                column: "InstagramId",
                principalSchema: "instagram",
                principalTable: "Instagram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Posts_PostInstagramId_PostId1",
                schema: "instagram",
                table: "Videos",
                columns: new[] { "PostInstagramId", "PostId1" },
                principalSchema: "instagram",
                principalTable: "Posts",
                principalColumns: new[] { "InstagramId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
