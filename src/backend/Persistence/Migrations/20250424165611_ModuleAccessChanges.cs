using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModuleAccessChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_accesses_modules_ModuleId",
                table: "module_accesses");

            migrationBuilder.DropForeignKey(
                name: "FK_module_accesses_users_UserId",
                table: "module_accesses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "module_accesses",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "module_accesses",
                newName: "module_id");

            migrationBuilder.RenameIndex(
                name: "IX_module_accesses_UserId",
                table: "module_accesses",
                newName: "IX_module_accesses_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_module_accesses_ModuleId",
                table: "module_accesses",
                newName: "IX_module_accesses_module_id");

            migrationBuilder.RenameIndex(
                name: "IX_module_accesses_module_access_id_ModuleId",
                table: "module_accesses",
                newName: "IX_module_accesses_module_access_id_module_id");

            migrationBuilder.AddColumn<int>(
                name: "module_longread_count",
                table: "modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "module_completion_date",
                table: "module_accesses",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<bool>(
                name: "is_module_available",
                table: "module_accesses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "test_tries_count",
                table: "module_accesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "longread_completions",
                columns: table => new
                {
                    longread_completion_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    resource_id = table.Column<int>(type: "integer", nullable: false),
                    ModuleAccessEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("longread_completions_pkey", x => x.longread_completion_id);
                    table.ForeignKey(
                        name: "FK_longread_completions_module_accesses_ModuleAccessEntityId",
                        column: x => x.ModuleAccessEntityId,
                        principalTable: "module_accesses",
                        principalColumn: "module_access_id");
                    table.ForeignKey(
                        name: "FK_longread_completions_resources_resource_id",
                        column: x => x.resource_id,
                        principalTable: "resources",
                        principalColumn: "resource_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_longread_completions_ModuleAccessEntityId",
                table: "longread_completions",
                column: "ModuleAccessEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_longread_completions_resource_id",
                table: "longread_completions",
                column: "resource_id");

            migrationBuilder.AddForeignKey(
                name: "FK_module_accesses_modules_module_id",
                table: "module_accesses",
                column: "module_id",
                principalTable: "modules",
                principalColumn: "module_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_module_accesses_users_user_id",
                table: "module_accesses",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_accesses_modules_module_id",
                table: "module_accesses");

            migrationBuilder.DropForeignKey(
                name: "FK_module_accesses_users_user_id",
                table: "module_accesses");

            migrationBuilder.DropTable(
                name: "longread_completions");

            migrationBuilder.DropColumn(
                name: "module_longread_count",
                table: "modules");

            migrationBuilder.DropColumn(
                name: "is_module_available",
                table: "module_accesses");

            migrationBuilder.DropColumn(
                name: "test_tries_count",
                table: "module_accesses");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "module_accesses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "module_id",
                table: "module_accesses",
                newName: "ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_module_accesses_user_id",
                table: "module_accesses",
                newName: "IX_module_accesses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_module_accesses_module_id",
                table: "module_accesses",
                newName: "IX_module_accesses_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_module_accesses_module_access_id_module_id",
                table: "module_accesses",
                newName: "IX_module_accesses_module_access_id_ModuleId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "module_completion_date",
                table: "module_accesses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_module_accesses_modules_ModuleId",
                table: "module_accesses",
                column: "ModuleId",
                principalTable: "modules",
                principalColumn: "module_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_module_accesses_users_UserId",
                table: "module_accesses",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
