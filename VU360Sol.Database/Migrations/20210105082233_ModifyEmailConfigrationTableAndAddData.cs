using Microsoft.EntityFrameworkCore.Migrations;

namespace VU360Sol.Database.Migrations
{
    public partial class ModifyEmailConfigrationTableAndAddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Eamil",
                table: "EmailConfigurations",
                newName: "Email");

            migrationBuilder.InsertData(
                table: "EmailConfigurations",
                columns: new[] { "Id", "Email", "Host", "Password", "Port" },
                values: new object[] { 1, "streamvitalsllc@gmail.com", "smtp.gmail.com", "VuSolution360", 587 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "EmailConfigurations",
                newName: "Eamil");
        }
    }
}
