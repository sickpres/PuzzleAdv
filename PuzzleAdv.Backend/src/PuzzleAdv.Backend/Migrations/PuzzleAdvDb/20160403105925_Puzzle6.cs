using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace PuzzleAdv.Backend.Migrations.PuzzleAdvDb
{
    public partial class Puzzle6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Prize_Shop_ShopId", table: "Prize");
            migrationBuilder.DropForeignKey(name: "FK_Puzzle_Shop_ShopId", table: "Puzzle");
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Opening2",
                table: "OpeningHours",
                nullable: true);
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Opening1",
                table: "OpeningHours",
                nullable: true);
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Closing2",
                table: "OpeningHours",
                nullable: true);
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Closing1",
                table: "OpeningHours",
                nullable: true);
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
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Opening2",
                table: "OpeningHours",
                nullable: false);
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Opening1",
                table: "OpeningHours",
                nullable: false);
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Closing2",
                table: "OpeningHours",
                nullable: false);
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Closing1",
                table: "OpeningHours",
                nullable: false);
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
