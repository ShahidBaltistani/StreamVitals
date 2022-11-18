using Microsoft.EntityFrameworkCore.Migrations;

namespace VU360Sol.Database.Migrations
{
    public partial class RenameDoctorFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Doctors",
                newName: "ProviderType");

            migrationBuilder.RenameColumn(
                name: "PracticeAddress",
                table: "Doctors",
                newName: "LocationName");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Doctors",
                newName: "LocationAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderType",
                table: "Doctors",
                newName: "Speciality");

            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "Doctors",
                newName: "PracticeAddress");

            migrationBuilder.RenameColumn(
                name: "LocationAddress",
                table: "Doctors",
                newName: "Location");
        }
    }
}
