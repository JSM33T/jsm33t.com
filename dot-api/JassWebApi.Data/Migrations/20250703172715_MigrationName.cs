using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JassWebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Blogs",
                newName: "PublishedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "Blogs",
                newName: "UpdatedAt");
        }
    }
}
