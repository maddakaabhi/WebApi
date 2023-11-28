using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Notebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Users_UserId",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "Notebook");

            migrationBuilder.RenameIndex(
                name: "IX_Note_UserId",
                table: "Notebook",
                newName: "IX_Notebook_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notebook",
                table: "Notebook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notebook_Users_UserId",
                table: "Notebook",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notebook_Users_UserId",
                table: "Notebook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notebook",
                table: "Notebook");

            migrationBuilder.RenameTable(
                name: "Notebook",
                newName: "Note");

            migrationBuilder.RenameIndex(
                name: "IX_Notebook_UserId",
                table: "Note",
                newName: "IX_Note_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Users_UserId",
                table: "Note",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
