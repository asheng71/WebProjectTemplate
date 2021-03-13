using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.SlopeSourceDb
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ss_tb_application_log",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ss_tb_application_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tr_tb_global_config_change_log",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    config_category = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reference_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    state_change = table.Column<int>(type: "int", nullable: false),
                    gca_oid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    security_server_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    om_serial_id = table.Column<long>(type: "bigint", nullable: true),
                    global_config_json = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tr_tb_global_config_change_log", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tr_tb_global_config_change_log_reference_id_config_category_state_change",
                table: "tr_tb_global_config_change_log",
                columns: new[] { "reference_id", "config_category", "state_change" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ss_tb_application_log");

            migrationBuilder.DropTable(
                name: "tr_tb_global_config_change_log");
        }
    }
}
