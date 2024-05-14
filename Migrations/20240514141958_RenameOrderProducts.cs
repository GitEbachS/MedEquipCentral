using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedEquipCentral.Migrations
{
    public partial class RenameOrderProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsProducts_Orders_OrderId",
                table: "ProductsProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsProducts_Products_ProductId",
                table: "ProductsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProducts",
                table: "ProductsProducts");

            migrationBuilder.RenameTable(
                name: "ProductsProducts",
                newName: "OrderProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProducts_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProducts_OrderId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "ProductsProducts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "ProductsProducts",
                newName: "IX_ProductsProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderId",
                table: "ProductsProducts",
                newName: "IX_ProductsProducts_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProducts",
                table: "ProductsProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsProducts_Orders_OrderId",
                table: "ProductsProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsProducts_Products_ProductId",
                table: "ProductsProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
