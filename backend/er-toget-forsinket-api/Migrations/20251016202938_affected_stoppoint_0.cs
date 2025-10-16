using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ertogetforsinketapi.Migrations
{
    /// <inheritdoc />
    public partial class affected_stoppoint_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "affected_vehicle_journey",
                schema: "train_delays");

            migrationBuilder.AddColumn<string>(
                name: "description_no",
                schema: "train_delays",
                table: "situation_exchange",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "end_time",
                schema: "train_delays",
                table: "situation_exchange",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "progress",
                schema: "train_delays",
                table: "situation_exchange",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "report_type",
                schema: "train_delays",
                table: "situation_exchange",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "start_time",
                schema: "train_delays",
                table: "situation_exchange",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "affected_stop_point",
                schema: "train_delays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dated_vehicle_journey_ref = table.Column<string>(type: "text", nullable: true),
                    stop_point_ref = table.Column<string>(type: "text", nullable: true),
                    stop_conditions = table.Column<List<string>>(type: "text[]", nullable: false),
                    SituationExchangeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affected_stop_point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_affected_stop_point_situation_exchange_SituationExchangeId",
                        column: x => x.SituationExchangeId,
                        principalSchema: "train_delays",
                        principalTable: "situation_exchange",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_affected_stop_point_SituationExchangeId",
                schema: "train_delays",
                table: "affected_stop_point",
                column: "SituationExchangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "affected_stop_point",
                schema: "train_delays");

            migrationBuilder.DropColumn(
                name: "description_no",
                schema: "train_delays",
                table: "situation_exchange");

            migrationBuilder.DropColumn(
                name: "end_time",
                schema: "train_delays",
                table: "situation_exchange");

            migrationBuilder.DropColumn(
                name: "progress",
                schema: "train_delays",
                table: "situation_exchange");

            migrationBuilder.DropColumn(
                name: "report_type",
                schema: "train_delays",
                table: "situation_exchange");

            migrationBuilder.DropColumn(
                name: "start_time",
                schema: "train_delays",
                table: "situation_exchange");

            migrationBuilder.CreateTable(
                name: "affected_vehicle_journey",
                schema: "train_delays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    situation_exchange_id = table.Column<int>(type: "integer", nullable: false),
                    dated_vehicle_journey_ref = table.Column<string>(type: "text", nullable: true),
                    stop_conditions = table.Column<string>(type: "text", nullable: true),
                    stop_point_ref = table.Column<string>(type: "text", nullable: true)
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
    }
}
