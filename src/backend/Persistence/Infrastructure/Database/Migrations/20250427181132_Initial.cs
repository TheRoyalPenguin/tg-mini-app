using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    book_title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    book_author = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    file_key = table.Column<string>(type: "text", nullable: false),
                    cover_key = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("books_pkey", x => x.book_id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    course_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("courses_pkey", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    role_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    module_title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    module_description = table.Column<string>(type: "text", nullable: false),
                    module_longread_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    course_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("modules_pkey", x => x.module_id);
                    table.ForeignKey(
                        name: "FK_modules_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_telegram_id = table.Column<long>(type: "bigint", nullable: false),
                    user_first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_banned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    registered_datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "longreads",
                columns: table => new
                {
                    longread_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    longread_title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    longread_description = table.Column<string>(type: "text", nullable: false),
                    html_content_key = table.Column<string>(type: "text", nullable: false),
                    original_docx_key = table.Column<string>(type: "text", nullable: false),
                    module_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("longreads_pkey", x => x.longread_id);
                    table.ForeignKey(
                        name: "fk_longreads_modules",
                        column: x => x.module_id,
                        principalTable: "modules",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    test_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    test_title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    json_key = table.Column<string>(type: "text", nullable: false),
                    module_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tests_pkey", x => x.test_id);
                    table.ForeignKey(
                        name: "fk_tests_modules",
                        column: x => x.module_id,
                        principalTable: "modules",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "enrollments",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_course_completed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    enrollment_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    course_completion_date = table.Column<DateOnly>(type: "date", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    course_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("enrollments_pkey", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK_enrollments_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_enrollments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "module_accesses",
                columns: table => new
                {
                    module_access_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    test_tries_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    is_module_completed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_module_available = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    module_completion_date = table.Column<DateOnly>(type: "date", nullable: true),
                    last_activity = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    module_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("module_accesses_pkey", x => x.module_access_id);
                    table.ForeignKey(
                        name: "FK_module_accesses_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "modules",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_module_accesses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "longread_images",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image_key = table.Column<string>(type: "text", nullable: false),
                    longread_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("longread_images_pkey", x => x.image_id);
                    table.ForeignKey(
                        name: "fk_images_longreads",
                        column: x => x.longread_id,
                        principalTable: "longreads",
                        principalColumn: "longread_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttemptNumber = table.Column<int>(type: "integer", nullable: false),
                    TotalQuestionsCount = table.Column<int>(type: "integer", nullable: false),
                    CorrectAnswersCount = table.Column<int>(type: "integer", nullable: false),
                    WrongAnswersCount = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_tests_TestId",
                        column: x => x.TestId,
                        principalTable: "tests",
                        principalColumn: "test_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "longread_completions",
                columns: table => new
                {
                    longread_completion_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    module_access_id = table.Column<int>(type: "integer", nullable: false),
                    longread_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("longread_completions_pkey", x => x.longread_completion_id);
                    table.ForeignKey(
                        name: "FK_longread_completions_longreads_longread_id",
                        column: x => x.longread_id,
                        principalTable: "longreads",
                        principalColumn: "longread_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_longread_completions_module_accesses_module_access_id",
                        column: x => x.module_access_id,
                        principalTable: "module_accesses",
                        principalColumn: "module_access_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_course_id",
                table: "enrollments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_user_id_course_id",
                table: "enrollments",
                columns: new[] { "user_id", "course_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_longread_completions_longread_id_module_access_id",
                table: "longread_completions",
                columns: new[] { "longread_id", "module_access_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_longread_completions_module_access_id",
                table: "longread_completions",
                column: "module_access_id");

            migrationBuilder.CreateIndex(
                name: "IX_longread_images_longread_id",
                table: "longread_images",
                column: "longread_id");

            migrationBuilder.CreateIndex(
                name: "IX_longreads_module_id",
                table: "longreads",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_module_accesses_module_access_id_module_id",
                table: "module_accesses",
                columns: new[] { "module_access_id", "module_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_module_accesses_module_id",
                table: "module_accesses",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_module_accesses_user_id",
                table: "module_accesses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_module_books_book_id",
                table: "module_books",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_modules_course_id",
                table: "modules",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_role_level",
                table: "roles",
                column: "role_level",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_role_name",
                table: "roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestId",
                table: "TestResults",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_UserId",
                table: "TestResults",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tests_module_id",
                table: "tests",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_user_phone_number",
                table: "users",
                column: "user_phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_telegram_id",
                table: "users",
                column: "user_telegram_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enrollments");

            migrationBuilder.DropTable(
                name: "longread_completions");

            migrationBuilder.DropTable(
                name: "longread_images");

            migrationBuilder.DropTable(
                name: "module_books");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "module_accesses");

            migrationBuilder.DropTable(
                name: "longreads");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "tests");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
