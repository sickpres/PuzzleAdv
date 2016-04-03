using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace PuzzleAdv.Backend.Migrations.PuzzleAdvDb
{
    public partial class Puzzle5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Prize_Shop_ShopId", table: "Prize");
            migrationBuilder.DropForeignKey(name: "FK_Puzzle_Shop_ShopId", table: "Puzzle");
            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Closing1 = table.Column<TimeSpan>(nullable: false),
                    Closing2 = table.Column<TimeSpan>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    Opening1 = table.Column<TimeSpan>(nullable: false),
                    Opening2 = table.Column<TimeSpan>(nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.ID);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Shop_ShopId",
                table: "Prize",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Puzzle_Shop_ShopId",
                table: "Puzzle",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Prize_Shop_ShopId", table: "Prize");
            migrationBuilder.DropForeignKey(name: "FK_Puzzle_Shop_ShopId", table: "Puzzle");
            migrationBuilder.DropTable("OpeningHours");
            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Shop_ShopId",
                table: "Prize",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Puzzle_Shop_ShopId",
                table: "Puzzle",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
