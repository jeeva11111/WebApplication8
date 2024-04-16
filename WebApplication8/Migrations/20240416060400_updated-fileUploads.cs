using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class updatedfileUploads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentUserId",
                table: "ImageUploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImageUploads_CurrentUserId",
                table: "ImageUploads",
                column: "CurrentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUploads_Users_CurrentUserId",
                table: "ImageUploads",
                column: "CurrentUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUploads_Users_CurrentUserId",
                table: "ImageUploads");

            migrationBuilder.DropIndex(
                name: "IX_ImageUploads_CurrentUserId",
                table: "ImageUploads");

            migrationBuilder.DropColumn(
                name: "CurrentUserId",
                table: "ImageUploads");
        }
    }
}
