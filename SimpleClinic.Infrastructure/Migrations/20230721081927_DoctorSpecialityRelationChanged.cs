using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleClinic.Infrastructure.Migrations
{
    public partial class DoctorSpecialityRelationChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsSpecialities");

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SpecialityId",
                table: "AspNetUsers",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specialities_SpecialityId",
                table: "AspNetUsers",
                column: "SpecialityId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specialities_SpecialityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SpecialityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "DoctorsSpecialities",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsSpecialities", x => new { x.DoctorId, x.SpecialityId });
                    table.ForeignKey(
                        name: "FK_DoctorsSpecialities_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorsSpecialities_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsSpecialities_SpecialityId",
                table: "DoctorsSpecialities",
                column: "SpecialityId");
        }
    }
}
