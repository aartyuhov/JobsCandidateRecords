using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsCandidateRecords.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Candidate_CandidateId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CandidateId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CandidateId",
                table: "Notes",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Candidate_CandidateId",
                table: "Notes",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }
    }
}
