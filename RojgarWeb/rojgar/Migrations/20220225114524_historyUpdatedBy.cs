using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rojgar.Migrations
{
    public partial class historyUpdatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ApplicationHistories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "ApplicationHistories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationHistories_UpdatedBy",
                table: "ApplicationHistories",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationHistories_AspNetUsers_UpdatedBy",
                table: "ApplicationHistories",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationHistories_AspNetUsers_UpdatedBy",
                table: "ApplicationHistories");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationHistories_UpdatedBy",
                table: "ApplicationHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ApplicationHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "ApplicationHistories");
        }
    }
}
