using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JassWebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuth4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserSessionId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserSessionId",
                table: "RefreshTokens",
                column: "UserSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserSessionId",
                table: "RefreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserSessionId",
                table: "RefreshTokens",
                column: "UserSessionId",
                unique: true);
        }
    }
}
