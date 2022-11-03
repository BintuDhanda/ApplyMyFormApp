using Microsoft.EntityFrameworkCore.Migrations;

namespace rojgar.Migrations
{
    public partial class updateQualificationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Board",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "Division",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "ObtainMark",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "PassYear",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "QualificationLevel",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "RollNo",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "TotalMark",
                table: "Qualifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Board",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ObtainMark",
                table: "Qualifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PassYear",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Percent",
                table: "Qualifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QualificationLevel",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RollNo",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalMark",
                table: "Qualifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
