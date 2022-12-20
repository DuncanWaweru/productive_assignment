using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Productive.Server.Data.Migrations
{
    public partial class makePhoneNumberUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClientCellPhones_CellPhone",
                table: "ClientCellPhones",
                column: "CellPhone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientCellPhones_CellPhone",
                table: "ClientCellPhones");
        }
    }
}
