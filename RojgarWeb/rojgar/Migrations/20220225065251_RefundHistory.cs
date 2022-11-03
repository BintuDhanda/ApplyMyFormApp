using Microsoft.EntityFrameworkCore.Migrations;

namespace rojgar.Migrations
{
    public partial class RefundHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefundHistories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentHistoryId = table.Column<long>(nullable: false),
                    ApplicationHistoryId = table.Column<long>(nullable: false),
                    TransactionId = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    Amount = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundHistories_ApplicationHistories_ApplicationHistoryId",
                        column: x => x.ApplicationHistoryId,
                        principalTable: "ApplicationHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundHistories_PaymentHistories_PaymentHistoryId",
                        column: x => x.PaymentHistoryId,
                        principalTable: "PaymentHistories",
                        principalColumn: "Id");
                        //onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefundHistories_ApplicationHistoryId",
                table: "RefundHistories",
                column: "ApplicationHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundHistories_PaymentHistoryId",
                table: "RefundHistories",
                column: "PaymentHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefundHistories");
        }
    }
}
