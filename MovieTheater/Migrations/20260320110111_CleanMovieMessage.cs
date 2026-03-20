using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Migrations
{
    /// <inheritdoc />
    public partial class CleanMovieMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "MovieMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "MovieMessages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
