using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Api.Migrations
{
    /// <inheritdoc />
    public partial class newday1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    CoachID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoachName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.CoachID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalSeat = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoachID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusID);
                    table.ForeignKey(
                        name: "FK_Buses_Coaches_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID");
                });

            migrationBuilder.CreateTable(
                name: "BusRoutes",
                columns: table => new
                {
                    RouteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromCityID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToCityID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CoachID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRoutes", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK_BusRoutes_Cities_FromCityID",
                        column: x => x.FromCityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusRoutes_Cities_ToCityID",
                        column: x => x.ToCityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusRoutes_Coaches_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "CoachID");
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Roles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Roles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Roles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusSchedules",
                columns: table => new
                {
                    ScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RouteID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusSchedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_BusSchedules_BusRoutes_RouteID",
                        column: x => x.RouteID,
                        principalTable: "BusRoutes",
                        principalColumn: "RouteID");
                    table.ForeignKey(
                        name: "FK_BusSchedules_Buses_BusID",
                        column: x => x.BusID,
                        principalTable: "Buses",
                        principalColumn: "BusID");
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassengerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengerMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_BookingDetails_BusSchedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "BusSchedules",
                        principalColumn: "ScheduleID");
                });

            migrationBuilder.CreateTable(
                name: "BookingSeatDetails",
                columns: table => new
                {
                    SeatID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSeatDetails", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK_BookingSeatDetails_BookingDetails_BookingID",
                        column: x => x.BookingID,
                        principalTable: "BookingDetails",
                        principalColumn: "BookingID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_ScheduleID",
                table: "BookingDetails",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeatDetails_BookingID",
                table: "BookingSeatDetails",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_CoachID",
                table: "Buses",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_CoachID",
                table: "BusRoutes",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_FromCityID",
                table: "BusRoutes",
                column: "FromCityID");

            migrationBuilder.CreateIndex(
                name: "IX_BusRoutes_ToCityID",
                table: "BusRoutes",
                column: "ToCityID");

            migrationBuilder.CreateIndex(
                name: "IX_BusSchedules_BusID",
                table: "BusSchedules",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusSchedules_RouteID",
                table: "BusSchedules",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_RoleID",
                table: "User_Roles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_UserID",
                table: "User_Roles",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSeatDetails");

            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BusSchedules");

            migrationBuilder.DropTable(
                name: "BusRoutes");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Coaches");
        }
    }
}
