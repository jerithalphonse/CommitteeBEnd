using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Witness",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageUrl = table.Column<string>(nullable: true),
                    UploadedBy = table.Column<int>(nullable: true),
                    UploadedTime = table.Column<string>(nullable: true),
                    WilayatCode = table.Column<string>(nullable: true),
                    PollingStationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Witness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Witness_PollingStations_PollingStationID",
                        column: x => x.PollingStationID,
                        principalTable: "PollingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Witness_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Witness_Wilayats_WilayatCode",
                        column: x => x.WilayatCode,
                        principalTable: "Wilayats",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Witness_PollingStationID",
                table: "Witness",
                column: "PollingStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Witness_UploadedBy",
                table: "Witness",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Witness_WilayatCode",
                table: "Witness",
                column: "WilayatCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Witness");

        }
    }
}
