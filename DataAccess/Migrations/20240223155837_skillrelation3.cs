using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class skillrelation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Skills");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "SoftSkills");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_ResumeId",
                table: "SoftSkills",
                newName: "IX_SoftSkills_ResumeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoftSkills",
                table: "SoftSkills",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HardSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResumeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardSkills_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardSkills_ResumeId",
                table: "HardSkills",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoftSkills_Resumes_ResumeId",
                table: "SoftSkills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoftSkills_Resumes_ResumeId",
                table: "SoftSkills");

            migrationBuilder.DropTable(
                name: "HardSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoftSkills",
                table: "SoftSkills");

            migrationBuilder.RenameTable(
                name: "SoftSkills",
                newName: "Skills");

            migrationBuilder.RenameIndex(
                name: "IX_SoftSkills_ResumeId",
                table: "Skills",
                newName: "IX_Skills_ResumeId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Skills",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Resumes_ResumeId",
                table: "Skills",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
