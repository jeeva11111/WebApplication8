using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedtaskPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "ImageFile");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ImageFile",
                newName: "ImageType");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ImageFile",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ImageFile");

            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "ImageFile",
                newName: "FileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "ImageFile",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
