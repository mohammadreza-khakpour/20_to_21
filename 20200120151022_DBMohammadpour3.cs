using Microsoft.EntityFrameworkCore.Migrations;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Migrations
{
    public partial class DBMohammadpour3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchasecarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isPaid = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchasecarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchasecarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchasecartProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    count = table.Column<int>(nullable: false),
                    PurchasecartId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasecartProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasecartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasecartProducts_Purchasecarts_PurchasecartId",
                        column: x => x.PurchasecartId,
                        principalTable: "Purchasecarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasecartProducts_ProductId",
                table: "PurchasecartProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasecartProducts_PurchasecartId",
                table: "PurchasecartProducts",
                column: "PurchasecartId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchasecarts_UserId",
                table: "Purchasecarts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasecartProducts");

            migrationBuilder.DropTable(
                name: "Purchasecarts");
        }
    }
}
