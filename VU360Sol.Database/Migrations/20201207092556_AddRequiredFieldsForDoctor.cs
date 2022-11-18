using Microsoft.EntityFrameworkCore.Migrations;

namespace VU360Sol.Database.Migrations
{
    public partial class AddRequiredFieldsForDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentCount",
                table: "Doctors",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Credentials",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationAddress",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationCity",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationCode",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationState",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationZip",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NPI",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Networks",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadedStatus",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentCount",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Credentials",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LocationAddress",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LocationCity",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LocationCode",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LocationState",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LocationZip",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "NPI",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Networks",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "UploadedStatus",
                table: "Doctors");
        }
    }
}
