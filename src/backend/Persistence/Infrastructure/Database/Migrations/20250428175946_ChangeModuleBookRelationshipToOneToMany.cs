using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModuleBookRelationshipToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "module_books");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_books_ModuleId",
                table: "books",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_modules_ModuleId",
                table: "books",
                column: "ModuleId",
                principalTable: "modules",
                principalColumn: "module_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_modules_ModuleId",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_ModuleId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "books");

            migrationBuilder.CreateTable(
                name: "module_books",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "integer", nullable: false),
                    book_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("module_books_pkey", x => new { x.module_id, x.book_id });
                    table.ForeignKey(
                        name: "fk_module_books_books",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_module_books_modules",
                        column: x => x.module_id,
                        principalTable: "modules",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_module_books_book_id",
                table: "module_books",
                column: "book_id");
        }
    }
}
