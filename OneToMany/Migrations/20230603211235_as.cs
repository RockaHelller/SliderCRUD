using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneToMany.Migrations
{
    public partial class @as : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 4, 1, 12, 35, 423, DateTimeKind.Local).AddTicks(7874));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 4, 1, 12, 35, 423, DateTimeKind.Local).AddTicks(7886));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 4, 1, 12, 35, 423, DateTimeKind.Local).AddTicks(7887));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sliders");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 1, 21, 9, 56, 55, DateTimeKind.Local).AddTicks(2227));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 1, 21, 9, 56, 55, DateTimeKind.Local).AddTicks(2246));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 6, 1, 21, 9, 56, 55, DateTimeKind.Local).AddTicks(2247));
        }
    }
}
