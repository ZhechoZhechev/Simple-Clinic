using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleClinic.Infrastructure.Migrations
{
    public partial class FormCompletedAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FormsCompleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormsCompleted",
                table: "AspNetUsers");
        }
    }
}
