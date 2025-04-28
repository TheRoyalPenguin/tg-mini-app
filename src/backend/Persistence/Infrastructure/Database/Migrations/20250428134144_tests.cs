using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class tests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_tests_TestId",
                table: "TestResults");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_users_UserId",
                table: "TestResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestResults",
                table: "TestResults");

            migrationBuilder.RenameTable(
                name: "TestResults",
                newName: "test_results");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "test_results",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "test_results",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "WrongAnswersCount",
                table: "test_results",
                newName: "wrong_answers_count");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "test_results",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TotalQuestionsCount",
                table: "test_results",
                newName: "total_questions_count");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "test_results",
                newName: "test_id");

            migrationBuilder.RenameColumn(
                name: "CorrectAnswersCount",
                table: "test_results",
                newName: "correct_answers_count");

            migrationBuilder.RenameColumn(
                name: "AttemptNumber",
                table: "test_results",
                newName: "attempt_number");

            migrationBuilder.RenameIndex(
                name: "IX_TestResults_UserId",
                table: "test_results",
                newName: "IX_test_results_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_TestResults_TestId",
                table: "test_results",
                newName: "IX_test_results_test_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_test_results",
                table: "test_results",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_test_results_tests_test_id",
                table: "test_results",
                column: "test_id",
                principalTable: "tests",
                principalColumn: "test_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_test_results_users_user_id",
                table: "test_results",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_test_results_tests_test_id",
                table: "test_results");

            migrationBuilder.DropForeignKey(
                name: "FK_test_results_users_user_id",
                table: "test_results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_test_results",
                table: "test_results");

            migrationBuilder.RenameTable(
                name: "test_results",
                newName: "TestResults");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "TestResults",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "TestResults",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "wrong_answers_count",
                table: "TestResults",
                newName: "WrongAnswersCount");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "TestResults",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "total_questions_count",
                table: "TestResults",
                newName: "TotalQuestionsCount");

            migrationBuilder.RenameColumn(
                name: "test_id",
                table: "TestResults",
                newName: "TestId");

            migrationBuilder.RenameColumn(
                name: "correct_answers_count",
                table: "TestResults",
                newName: "CorrectAnswersCount");

            migrationBuilder.RenameColumn(
                name: "attempt_number",
                table: "TestResults",
                newName: "AttemptNumber");

            migrationBuilder.RenameIndex(
                name: "IX_test_results_user_id",
                table: "TestResults",
                newName: "IX_TestResults_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_test_results_test_id",
                table: "TestResults",
                newName: "IX_TestResults_TestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestResults",
                table: "TestResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_tests_TestId",
                table: "TestResults",
                column: "TestId",
                principalTable: "tests",
                principalColumn: "test_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_users_UserId",
                table: "TestResults",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
