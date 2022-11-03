using Microsoft.EntityFrameworkCore.Migrations;

namespace rojgar.Migrations
{
    public partial class admitcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdmitCardUrl",
                table: "ApplicationHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmitCardUrl",
                table: "ApplicationHistories");
        }
    }
}
