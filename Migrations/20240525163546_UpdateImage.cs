using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedEquipCentral.Migrations
{
    public partial class UpdateImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CloseDate",
                value: new DateTime(2024, 5, 23, 11, 35, 45, 733, DateTimeKind.Local).AddTicks(6674));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CloseDate",
                value: new DateTime(2024, 5, 24, 11, 35, 45, 733, DateTimeKind.Local).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://m.media-amazon.com/images/I/21bk5VPlZlL.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://firstaidsuppliesonline.com/wp-content/uploads/2023/10/68-BANSI-RR-435x435.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://cdn11.bigcommerce.com/s-kslxuc4w/images/stencil/500x659/products/1047/5929/lite_open__68268.1705045702.png?c=2");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 5, 25, 16, 35, 45, 733, DateTimeKind.Utc).AddTicks(6798));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 5, 25, 16, 35, 45, 733, DateTimeKind.Utc).AddTicks(6801));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CloseDate",
                value: new DateTime(2024, 5, 16, 13, 40, 17, 562, DateTimeKind.Local).AddTicks(2733));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CloseDate",
                value: new DateTime(2024, 5, 17, 13, 40, 17, 562, DateTimeKind.Local).AddTicks(2790));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://www.life-assist.com/Content/ProductImages/960x720/04_IT8052_01.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://www.life-assist.com/Content/ProductImages/460x345/05_fj8000new.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://www.life-assist.com/Content/ProductImages/460x345/02_CHI708050_tstStrip.jpg");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 5, 18, 18, 40, 17, 562, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 5, 18, 18, 40, 17, 562, DateTimeKind.Utc).AddTicks(2825));
        }
    }
}
