using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoDice.Migrations
{
    /// <inheritdoc />
    public partial class AddWebUserSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebUserSession_Players_PlayerId",
                table: "WebUserSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WebUserSession",
                table: "WebUserSession");

            migrationBuilder.RenameTable(
                name: "WebUserSession",
                newName: "WebUserSessions");

            migrationBuilder.RenameIndex(
                name: "IX_WebUserSession_PlayerId",
                table: "WebUserSessions",
                newName: "IX_WebUserSessions_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebUserSessions",
                table: "WebUserSessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WebUserSessions_Players_PlayerId",
                table: "WebUserSessions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebUserSessions_Players_PlayerId",
                table: "WebUserSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WebUserSessions",
                table: "WebUserSessions");

            migrationBuilder.RenameTable(
                name: "WebUserSessions",
                newName: "WebUserSession");

            migrationBuilder.RenameIndex(
                name: "IX_WebUserSessions_PlayerId",
                table: "WebUserSession",
                newName: "IX_WebUserSession_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebUserSession",
                table: "WebUserSession",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WebUserSession_Players_PlayerId",
                table: "WebUserSession",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
