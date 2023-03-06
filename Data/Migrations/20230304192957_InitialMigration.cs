using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppContact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppTelephoneNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Phonenumber = table.Column<string>(type: "text", nullable: true),
                    AppContactId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTelephoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTelephoneNumber_AppContact_AppContactId",
                        column: x => x.AppContactId,
                        principalTable: "AppContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppContact_Address",
                table: "AppContact",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppContact_Name",
                table: "AppContact",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTelephoneNumber_AppContactId",
                table: "AppTelephoneNumber",
                column: "AppContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTelephoneNumber");

            migrationBuilder.DropTable(
                name: "AppContact");
        }
    }
}
