using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PuzzleAdv.Backend.Models;

namespace PuzzleAdv.Backend.Migrations.PuzzleAdvDb
{
    [DbContext(typeof(PuzzleAdvDbContext))]
    [Migration("20160403105925_Puzzle6")]
    partial class Puzzle6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PuzzleAdv.Backend.Models.OpeningHours", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan?>("Closing1");

                    b.Property<TimeSpan?>("Closing2");

                    b.Property<int>("DayOfWeek");

                    b.Property<bool>("IsClosed");

                    b.Property<TimeSpan?>("Opening1");

                    b.Property<TimeSpan?>("Opening2");

                    b.Property<int>("ShopId");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("PuzzleAdv.Backend.Models.Prize", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("DiscountPrice");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("LongDesc");

                    b.Property<int>("NeededPoints");

                    b.Property<decimal>("OriginalPrice");

                    b.Property<string>("PrizeImage");

                    b.Property<int>("ShopId");

                    b.Property<string>("ShortDesc");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("PuzzleAdv.Backend.Models.Puzzle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("DeleteUserId");

                    b.Property<int>("Distance");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime>("InsertDate");

                    b.Property<string>("InsertUserId");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("LastUpdateUserId");

                    b.Property<string>("PuzzleImage");

                    b.Property<int>("ShopId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("PuzzleAdv.Backend.Models.Shop", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime?>("DeleteDate");

                    b.Property<string>("DeleteUserId");

                    b.Property<DateTime>("InsertDate");

                    b.Property<string>("InsertUserId");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("LastUpdateUserId");

                    b.Property<double>("Latitude");

                    b.Property<string>("LongDesc");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("ShortDesc");

                    b.Property<string>("UserId");

                    b.Property<string>("Website");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("PuzzleAdv.Backend.Models.Prize", b =>
                {
                    b.HasOne("PuzzleAdv.Backend.Models.Shop")
                        .WithMany()
                        .HasForeignKey("ShopId");
                });

            modelBuilder.Entity("PuzzleAdv.Backend.Models.Puzzle", b =>
                {
                    b.HasOne("PuzzleAdv.Backend.Models.Shop")
                        .WithMany()
                        .HasForeignKey("ShopId");
                });
        }
    }
}
