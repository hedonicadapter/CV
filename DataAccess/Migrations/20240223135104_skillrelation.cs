using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class skillrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Resumes_ResumeId1",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ResumeId1",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ResumeId1",
                table: "Skills");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Skills",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Skills");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeId",
                table: "Skills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId1",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ResumeId1",
                table: "Skills",
                column: "ResumeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Resumes_ResumeId1",
                table: "Skills",
                column: "ResumeId1",
                principalTable: "Resumes",
                principalColumn: "Id");
        }
    }
}
