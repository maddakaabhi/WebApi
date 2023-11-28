using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelsT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Labels_NotesDbId",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "NotesDbId",
                table: "Labels");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "LabelsT");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_UserId",
                table: "LabelsT",
                newName: "IX_LabelsT_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelsT",
                table: "LabelsT",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelsT_Id",
                table: "LabelsT",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabelsT_Notebook_Id",
                table: "LabelsT",
                column: "Id",
                principalTable: "Notebook",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LabelsT_Users_UserId",
                table: "LabelsT",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabelsT_Notebook_Id",
                table: "LabelsT");

            migrationBuilder.DropForeignKey(
                name: "FK_LabelsT_Users_UserId",
                table: "LabelsT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelsT",
                table: "LabelsT");

            migrationBuilder.DropIndex(
                name: "IX_LabelsT_Id",
                table: "LabelsT");

            migrationBuilder.RenameTable(
                name: "LabelsT",
                newName: "Labels");

            migrationBuilder.RenameIndex(
                name: "IX_LabelsT_UserId",
                table: "Labels",
                newName: "IX_Labels_UserId");

            migrationBuilder.AddColumn<int>(
                name: "NotesDbId",
                table: "Labels",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_NotesDbId",
                table: "Labels",
                column: "NotesDbId");

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
    }
}
