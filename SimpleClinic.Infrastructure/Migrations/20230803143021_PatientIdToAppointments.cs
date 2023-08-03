using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleClinic.Infrastructure.Migrations
{
    public partial class PatientIdToAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAppointments_AspNetUsers_PatientId",
                table: "DoctorAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_AspNetUsers_PatientId",
                table: "ServiceAppointments");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "ServiceAppointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "DoctorAppointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAppointments_AspNetUsers_PatientId",
                table: "DoctorAppointments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_AspNetUsers_PatientId",
                table: "ServiceAppointments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAppointments_AspNetUsers_PatientId",
                table: "DoctorAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_AspNetUsers_PatientId",
                table: "ServiceAppointments");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "ServiceAppointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "DoctorAppointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAppointments_AspNetUsers_PatientId",
                table: "DoctorAppointments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_AspNetUsers_PatientId",
                table: "ServiceAppointments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
