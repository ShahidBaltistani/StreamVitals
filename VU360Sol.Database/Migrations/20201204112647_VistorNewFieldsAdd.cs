using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VU360Sol.Database.Migrations
{
    public partial class VistorNewFieldsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_SentEmails_SentEmailId",
                table: "Visitors");

            migrationBuilder.DropTable(
                name: "SentEmails");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_SentEmailId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "SentEmailId",
                table: "Visitors");

            migrationBuilder.AddColumn<string>(
                name: "Practice",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Practice",
                table: "Visitors");

            migrationBuilder.AddColumn<int>(
                name: "SentEmailId",
                table: "Visitors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SentEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentEmails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_SentEmailId",
                table: "Visitors",
                column: "SentEmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_SentEmails_SentEmailId",
                table: "Visitors",
                column: "SentEmailId",
                principalTable: "SentEmails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
