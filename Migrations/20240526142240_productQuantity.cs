using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedEquipCentral.Migrations
{
    public partial class productQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CloseDate",
                value: new DateTime(2024, 5, 24, 9, 22, 40, 366, DateTimeKind.Local).AddTicks(7104));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CloseDate",
                value: new DateTime(2024, 5, 25, 9, 22, 40, 366, DateTimeKind.Local).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 5, 26, 14, 22, 40, 366, DateTimeKind.Utc).AddTicks(7227));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 5, 26, 14, 22, 40, 366, DateTimeKind.Utc).AddTicks(7230));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderProducts");

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
    }
}
