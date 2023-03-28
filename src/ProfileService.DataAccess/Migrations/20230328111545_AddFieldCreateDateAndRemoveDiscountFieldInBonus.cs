using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.DataAccess.Migrations
{
    public partial class AddFieldCreateDateAndRemoveDiscountFieldInBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Bonus");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Bonus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Bonus");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Bonus",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
