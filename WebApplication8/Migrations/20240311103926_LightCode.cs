using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class LightCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chennels",
                columns: table => new
                {
                    ChennelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChennelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChennelDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categorey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chennels", x => x.ChennelId);
                    table.ForeignKey(
                        name: "FK_Chennels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoTitle = table.Column<string>(type: "nvarchar(355)", maxLength: 355, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5355, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    VideoType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChennelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Chennels_ChennelId",
                        column: x => x.ChennelId,
                        principalTable: "Chennels",
                        principalColumn: "ChennelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chennels_UserId",
                table: "Chennels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ChennelId",
                table: "Videos",
                column: "ChennelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Chennels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
