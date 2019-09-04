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
                name: "Messaging",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    By = table.Column<int>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    WilayatCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messaging", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messaging_Users_By",
                        column: x => x.By,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messaging_Wilayats_WilayatCode",
                        column: x => x.WilayatCode,
                        principalTable: "Wilayats",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Messaging_By",
                table: "Messaging",
                column: "By");

            migrationBuilder.CreateIndex(
                name: "IX_Messaging_WilayatCode",
                table: "Messaging",
                column: "WilayatCode");

        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messaging");

        }
    }
}
