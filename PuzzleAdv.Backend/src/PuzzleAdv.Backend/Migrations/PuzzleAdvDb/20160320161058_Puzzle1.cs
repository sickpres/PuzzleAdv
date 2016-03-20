using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace PuzzleAdv.Backend.Migrations.PuzzleAdvDb
{
    public partial class Puzzle1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    DeleteUserId = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    InsertUserId = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateUserId = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    LongDesc = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ShortDesc = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "Prize",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiscountPrice = table.Column<decimal>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    LongDesc = table.Column<string>(nullable: true),
                    NeededPoints = table.Column<int>(nullable: false),
                    OriginalPrice = table.Column<decimal>(nullable: false),
                    PrizeImage = table.Column<string>(nullable: true),
                    ShopId = table.Column<int>(nullable: false),
                    ShortDesc = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prize", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prize_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Puzzle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    DeleteUserId = table.Column<string>(nullable: true),
                    Distance = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    InsertUserId = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateUserId = table.Column<string>(nullable: true),
                    PuzzleImage = table.Column<string>(nullable: true),
                    ShopId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puzzle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Puzzle_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Prize");
            migrationBuilder.DropTable("Puzzle");
            migrationBuilder.DropTable("Shop");
        }
    }
}
