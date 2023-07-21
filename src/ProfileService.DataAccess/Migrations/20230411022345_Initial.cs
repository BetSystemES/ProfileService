using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileDatas",
                columns: table => new
                {
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDatas", x => x.profile_id);
                });

            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    bonus_id = table.Column<Guid>(type: "uuid", nullable: false),
                    profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_already_used = table.Column<bool>(type: "boolean", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    discount_type = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.bonus_id);
                    table.ForeignKey(
                        name: "FK_Bonuses_ProfileDatas_profile_id",
                        column: x => x.profile_id,
                        principalTable: "ProfileDatas",
                        principalColumn: "profile_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_profile_id",
                table: "Bonuses",
                column: "profile_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropTable(
                name: "ProfileDatas");
        }
    }
}
