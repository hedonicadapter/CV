using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PolymorphismRollbackIFear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Resumes_Education_ResumeId1",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Resumes_Project_ResumeId1",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Resumes_ResumeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Resumes_ResumeId1",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Education_ResumeId1",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Project_ResumeId1",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ResumeId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Education_ResumeId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Project_ResumeId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ResumeId1",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ResumeId",
                table: "Projects",
                newName: "IX_Projects_ResumeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Degree = table.Column<string>(type: "TEXT", nullable: false),
                    ResumeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Subtitle = table.Column<string>(type: "TEXT", nullable: true),
                    Dates = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ResumeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Subtitle = table.Column<string>(type: "TEXT", nullable: true),
                    Dates = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_ResumeId",
                table: "Educations",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ResumeId",
                table: "Experiences",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Resumes_ResumeId",
                table: "Projects",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Resumes_ResumeId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ResumeId",
                table: "Items",
                newName: "IX_Items_ResumeId");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Education_ResumeId1",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Project_ResumeId1",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId1",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Education_ResumeId1",
                table: "Items",
                column: "Education_ResumeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Project_ResumeId1",
                table: "Items",
                column: "Project_ResumeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ResumeId1",
                table: "Items",
                column: "ResumeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Resumes_Education_ResumeId1",
                table: "Items",
                column: "Education_ResumeId1",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Resumes_Project_ResumeId1",
                table: "Items",
                column: "Project_ResumeId1",
                principalTable: "Resumes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Resumes_ResumeId",
                table: "Items",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Resumes_ResumeId1",
                table: "Items",
                column: "ResumeId1",
                principalTable: "Resumes",
                principalColumn: "Id");
        }
    }
}
