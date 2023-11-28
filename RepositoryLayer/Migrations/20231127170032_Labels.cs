using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Labels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Notebook_NotesDbId",
                table: "Label");

            migrationBuilder.DropForeignKey(
                name: "FK_Label_Users_UserId",
                table: "Label");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Label",
                table: "Label");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Label");

            migrationBuilder.RenameTable(
                name: "Label",
                newName: "Labels");

            migrationBuilder.RenameIndex(
                name: "IX_Label_UserId",
                table: "Labels",
                newName: "IX_Labels_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Label_NotesDbId",
                table: "Labels",
                newName: "IX_Labels_NotesDbId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Labels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Notebook_NotesDbId",
                table: "Labels",
                column: "NotesDbId",
                principalTable: "Notebook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Notebook_NotesDbId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_UserId",
                table: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Labels");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "Label");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_UserId",
                table: "Label",
                newName: "IX_Label_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_NotesDbId",
                table: "Label",
                newName: "IX_Label_NotesDbId");

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Label",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Label",
                table: "Label",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Notebook_NotesDbId",
                table: "Label",
                column: "NotesDbId",
                principalTable: "Notebook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Users_UserId",
                table: "Label",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
