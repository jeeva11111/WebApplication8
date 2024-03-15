using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class updatedNotesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "FolderImagePath",
                table: "Notes",
                newName: "ImageType");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Notes",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "Notes",
                newName: "FolderImagePath");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Notes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
