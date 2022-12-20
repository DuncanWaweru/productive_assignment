using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Productive.Server.Data.Migrations
{
    public partial class updateEmailSmsStatusToEmailCellPhoneStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailSmsStatus",
                table: "ClientCellPhones",
                newName: "CellPhoneStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CellPhoneStatus",
                table: "ClientCellPhones",
                newName: "EmailSmsStatus");
        }
    }
}
