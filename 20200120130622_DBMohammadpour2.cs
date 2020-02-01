using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Migrations
{
    public partial class DBMohammadpour2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "img",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "imgThumbnail",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "imgThumbnail",
                table: "Products");
        }
    }
}
