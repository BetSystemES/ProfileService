using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalData",
                columns: table => new
                {
                    PersonalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalData", x => x.PersonalId);
                });

            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    BonusId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uuid", nullable: false),
                    isAlreadyUsed = table.Column<bool>(type: "boolean", nullable: false),
                    DiscountType = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.BonusId);
                    table.ForeignKey(
                        name: "FK_Bonus_PersonalData_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "PersonalData",
                        principalColumn: "PersonalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_PersonalId",
                table: "Bonus",
                column: "PersonalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "PersonalData");
        }
    }
}
