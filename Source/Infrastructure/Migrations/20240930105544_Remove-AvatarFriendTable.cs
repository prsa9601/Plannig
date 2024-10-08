using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAvatarFriendTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvatarFriend",
                schema: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvatarFriend",
                schema: "user",
                columns: table => new
                {
                    UserFriendsUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserFriendsId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    avatar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarFriend", x => new { x.UserFriendsUserId, x.UserFriendsId });
                    table.ForeignKey(
                        name: "FK_AvatarFriend_friends_UserFriendsUserId_UserFriendsId",
                        columns: x => new { x.UserFriendsUserId, x.UserFriendsId },
                        principalSchema: "user",
                        principalTable: "friends",
                        principalColumns: new[] { "UserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvatarFriend_UserId",
                schema: "user",
                table: "AvatarFriend",
                column: "UserId");
        }
    }
}
