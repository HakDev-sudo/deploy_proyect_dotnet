using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArcheryAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "certificates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    verification_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    pdf_url = table.Column<string>(type: "text", nullable: true),
                    blob_file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    issued_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    issued_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("certificates_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_certificate_issued_by",
                        column: x => x.issued_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_certificate_user",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "certificates_verification_code_key",
                table: "certificates",
                column: "verification_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_certificates_user",
                table: "certificates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_certificates_issued_by_id",
                table: "certificates",
                column: "issued_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "certificates");
        }
    }
}
