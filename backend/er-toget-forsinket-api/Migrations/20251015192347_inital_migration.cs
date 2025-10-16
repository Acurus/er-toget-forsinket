using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ertogetforsinketapi.Migrations
{
    /// <inheritdoc />
    public partial class inital_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "train_delays");

            migrationBuilder.CreateTable(
                name: "estimated_time_table_raw",
                schema: "train_delays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RawMessage = table.Column<string>(type: "TEXT", nullable: false),
                    ReceivedDateTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estimated_time_table_raw", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "situation_exchange",
                schema: "train_delays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creation_time = table.Column<string>(type: "text", nullable: true),
                    participant_ref = table.Column<string>(type: "text", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: true),
                    situation_number = table.Column<string>(type: "text", nullable: true),
                    version = table.Column<string>(type: "text", nullable: true),
                    versioned_at_time = table.Column<string>(type: "text", nullable: true),
                    summary_no = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_situation_exchange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "situation_exchange_raw",
                schema: "train_delays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RawMessage = table.Column<string>(type: "TEXT", nullable: false),
                    ReceivedDateTime = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_situation_exchange_raw", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "affected_vehicle_journey",
                schema: "train_delays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dated_vehicle_journey_ref = table.Column<string>(type: "text", nullable: true),
                    stop_point_ref = table.Column<string>(type: "text", nullable: true),
                    stop_conditions = table.Column<string>(type: "text", nullable: true),
                    situation_exchange_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affected_vehicle_journey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_affected_vehicle_journey_situation_exchange_situation_excha~",
                        column: x => x.situation_exchange_id,
                        principalSchema: "train_delays",
                        principalTable: "situation_exchange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_affected_vehicle_journey_situation_exchange_id",
                schema: "train_delays",
                table: "affected_vehicle_journey",
                column: "situation_exchange_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "affected_vehicle_journey",
                schema: "train_delays");

            migrationBuilder.DropTable(
                name: "estimated_time_table_raw",
                schema: "train_delays");

            migrationBuilder.DropTable(
                name: "situation_exchange_raw",
                schema: "train_delays");

            migrationBuilder.DropTable(
                name: "situation_exchange",
                schema: "train_delays");
        }
    }
}
