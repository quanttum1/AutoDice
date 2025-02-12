using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoDice.Migrations
{
    /// <inheritdoc />
    public partial class AddLuckWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TgUser_Players_PlayerId",
                table: "TgUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TgUser",
                table: "TgUser");

            migrationBuilder.RenameTable(
                name: "TgUser",
                newName: "TgUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TgUser_PlayerId",
                table: "TgUsers",
                newName: "IX_TgUsers_PlayerId");

            migrationBuilder.AddColumn<double>(
                name: "LuckWeight",
                table: "Skills",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TgUsers",
                table: "TgUsers",
                column: "TgUserId");

            migrationBuilder.CreateTable(
                name: "TgTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TgTokens", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TgUsers_Players_PlayerId",
                table: "TgUsers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TgUsers_Players_PlayerId",
                table: "TgUsers");

            migrationBuilder.DropTable(
                name: "TgTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TgUsers",
                table: "TgUsers");

            migrationBuilder.DropColumn(
                name: "LuckWeight",
                table: "Skills");

            migrationBuilder.RenameTable(
                name: "TgUsers",
                newName: "TgUser");

            migrationBuilder.RenameIndex(
                name: "IX_TgUsers_PlayerId",
                table: "TgUser",
                newName: "IX_TgUser_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TgUser",
                table: "TgUser",
                column: "TgUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TgUser_Players_PlayerId",
                table: "TgUser",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
