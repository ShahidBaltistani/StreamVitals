using Microsoft.EntityFrameworkCore.Migrations;

namespace VU360Sol.Database.Migrations
{
    public partial class RemoveAndRenameDoctorFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationAddress",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "UploadedStatus",
                table: "Doctors",
                newName: "DoctorStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoctorStatus",
                table: "Doctors",
                newName: "UploadedStatus");

            migrationBuilder.AddColumn<string>(
                name: "LocationAddress",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
