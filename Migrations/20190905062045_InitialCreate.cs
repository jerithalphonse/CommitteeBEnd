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
                name: "Governorates",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ArabicName = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KiosksTab = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AttendanceTab = table.Column<bool>(nullable: false),
                    WitnessTab = table.Column<bool>(nullable: false),
                    MessageTab = table.Column<bool>(nullable: false),
                    AssignPollingStationTab = table.Column<bool>(nullable: false),
                    DirectionsTab = table.Column<bool>(nullable: false),
                    KiosksTabReassign = table.Column<bool>(nullable: false),
                    AttendanceTabReassign = table.Column<bool>(nullable: false),
                    ScanQRTab = table.Column<bool>(nullable: false),
                    WitnessTabAddMore = table.Column<bool>(nullable: false),
                    ScanQrTabSelfAssignKiosks = table.Column<bool>(nullable: false),
                    MessageTabAllMembers = table.Column<bool>(nullable: false),
                    MessageTabHeadCommittees = table.Column<bool>(nullable: false),
                    MessageTabToHeadCommittee = table.Column<bool>(nullable: false),
                    MessageTabWaliOfficers = table.Column<bool>(nullable: false),
                    MessageTabCheckMessage = table.Column<bool>(nullable: false),
                    MessageTabRestrictMessage = table.Column<bool>(nullable: false),
                    NotificationTab = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wilayats",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ArabicName = table.Column<string>(nullable: true),
                    GovernorateCode = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wilayats", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Wilayats_Governorates_GovernorateCode",
                        column: x => x.GovernorateCode,
                        principalTable: "Governorates",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PollingStations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ArabicName = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    WilayatCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsUnifiedPollingCenter = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollingStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollingStations_Wilayats_WilayatCode",
                        column: x => x.WilayatCode,
                        principalTable: "Wilayats",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kiosks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<string>(nullable: true),
                    PollingDayStatus = table.Column<int>(nullable: false),
                    OpenTime = table.Column<string>(nullable: true),
                    HasIssue = table.Column<bool>(nullable: false),
                    CloseTime = table.Column<string>(nullable: true),
                    UnlockCode = table.Column<string>(nullable: true),
                    WilayatCode = table.Column<string>(nullable: true),
                    PollingStationID = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsUnifiedKiosk = table.Column<bool>(nullable: false),
                    AreVotersPresentAsWitnesses = table.Column<bool>(nullable: false),
                    IsNoFingerprintKiosk = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kiosks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kiosks_PollingStations_PollingStationID",
                        column: x => x.PollingStationID,
                        principalTable: "PollingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kiosks_Wilayats_WilayatCode",
                        column: x => x.WilayatCode,
                        principalTable: "Wilayats",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameEnglish = table.Column<string>(nullable: true),
                    NameArabic = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CommiteeType = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    AttendedAt = table.Column<string>(nullable: true),
                    GovernorateCode = table.Column<string>(nullable: true),
                    WilayatCode = table.Column<string>(nullable: true),
                    PollingStationId = table.Column<int>(nullable: true),
                    KioskId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Governorates_GovernorateCode",
                        column: x => x.GovernorateCode,
                        principalTable: "Governorates",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Kiosks_KioskId",
                        column: x => x.KioskId,
                        principalTable: "Kiosks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_PollingStations_PollingStationId",
                        column: x => x.PollingStationId,
                        principalTable: "PollingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Wilayats_WilayatCode",
                        column: x => x.WilayatCode,
                        principalTable: "Wilayats",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KiosksAssign",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(nullable: true),
                    AssignedBy = table.Column<int>(nullable: true),
                    KioskId = table.Column<int>(nullable: true),
                    PollingStationID = table.Column<int>(nullable: true),
                    AttendanceStartedAt = table.Column<string>(nullable: true),
                    AttendanceCompletedAt = table.Column<string>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiosksAssign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KiosksAssign_Users_AssignedBy",
                        column: x => x.AssignedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KiosksAssign_Kiosks_KioskId",
                        column: x => x.KioskId,
                        principalTable: "Kiosks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KiosksAssign_Users_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KiosksAssign_PollingStations_PollingStationID",
                        column: x => x.PollingStationID,
                        principalTable: "PollingStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Kiosks_PollingStationID",
                table: "Kiosks",
                column: "PollingStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Kiosks_WilayatCode",
                table: "Kiosks",
                column: "WilayatCode");

            migrationBuilder.CreateIndex(
                name: "IX_KiosksAssign_AssignedBy",
                table: "KiosksAssign",
                column: "AssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_KiosksAssign_KioskId",
                table: "KiosksAssign",
                column: "KioskId");

            migrationBuilder.CreateIndex(
                name: "IX_KiosksAssign_MemberId",
                table: "KiosksAssign",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_KiosksAssign_PollingStationID",
                table: "KiosksAssign",
                column: "PollingStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Messaging_By",
                table: "Messaging",
                column: "By");

            migrationBuilder.CreateIndex(
                name: "IX_Messaging_WilayatCode",
                table: "Messaging",
                column: "WilayatCode");

            migrationBuilder.CreateIndex(
                name: "IX_PollingStations_WilayatCode",
                table: "PollingStations",
                column: "WilayatCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GovernorateCode",
                table: "Users",
                column: "GovernorateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_KioskId",
                table: "Users",
                column: "KioskId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PollingStationId",
                table: "Users",
                column: "PollingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WilayatCode",
                table: "Users",
                column: "WilayatCode");

            migrationBuilder.CreateIndex(
                name: "IX_Wilayats_GovernorateCode",
                table: "Wilayats",
                column: "GovernorateCode");

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
                name: "KiosksAssign");

            migrationBuilder.DropTable(
                name: "Messaging");

            migrationBuilder.DropTable(
                name: "Witness");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Kiosks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "PollingStations");

            migrationBuilder.DropTable(
                name: "Wilayats");

            migrationBuilder.DropTable(
                name: "Governorates");
        }
    }
}
