using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MythicalBooksAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ratings",
                table: "Books",
                newName: "RatingCount");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Books",
                newName: "AverageRating");

            migrationBuilder.AlterColumn<string>(
                name: "PhysicalFormat",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RatingCount",
                table: "Books",
                newName: "Ratings");

            migrationBuilder.RenameColumn(
                name: "AverageRating",
                table: "Books",
                newName: "Rating");

            migrationBuilder.AlterColumn<string>(
                name: "PhysicalFormat",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
