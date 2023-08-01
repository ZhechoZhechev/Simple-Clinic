using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleClinic.Infrastructure.Migrations
{
    public partial class medicalHistoryMedicamentsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicaments_MedicalHistories_MedicalHistoryId",
                table: "Medicaments");

            migrationBuilder.DropIndex(
                name: "IX_Medicaments_MedicalHistoryId",
                table: "Medicaments");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId",
                table: "Medicaments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalHistoryId",
                table: "Medicaments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicaments_MedicalHistoryId",
                table: "Medicaments",
                column: "MedicalHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicaments_MedicalHistories_MedicalHistoryId",
                table: "Medicaments",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id");
        }
    }
}
