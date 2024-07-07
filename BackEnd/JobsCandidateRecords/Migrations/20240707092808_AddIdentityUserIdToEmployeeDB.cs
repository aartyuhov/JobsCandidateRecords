using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsCandidateRecords.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityUserIdToEmployeeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityUserId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IdentityUserId1",
                table: "Employee",
                column: "IdentityUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_IdentityUserId1",
                table: "Employee",
                column: "IdentityUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_IdentityUserId1",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_IdentityUserId1",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "Employee");
        }
    }
}
