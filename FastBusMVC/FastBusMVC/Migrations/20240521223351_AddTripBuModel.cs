using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBusMVC.Migrations
{
    public partial class AddTripBuModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pass_word = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    admin_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "bus",
                columns: table => new
                {
                    bus_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bus_tybe = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("c18", x => x.bus_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    u_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    last_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    pass_word = table.Column<string>(type: "varchar(155)", unicode: false, maxLength: 155, nullable: false),
                    gendr = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    bath_date = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    u_image = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("c12", x => x.u_id);
                });

            migrationBuilder.CreateTable(
                name: "trip",
                columns: table => new
                {
                    trip_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deparure_city = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Deparure_data_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    arrival_city = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    arrival_date_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    trip_image = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    bus_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip", x => x.trip_id);
                    table.ForeignKey(
                        name: "c38",
                        column: x => x.bus_id,
                        principalTable: "Admin",
                        principalColumn: "admin_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "sp_Booking",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    u_id = table.Column<int>(type: "int", nullable: true),
                    booking_date_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    required_seats = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    perpose = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("c103", x => x.book_id);
                    table.ForeignKey(
                        name: "c105",
                        column: x => x.u_id,
                        principalTable: "User",
                        principalColumn: "u_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trip_id = table.Column<int>(type: "int", nullable: true),
                    u_id = table.Column<int>(type: "int", nullable: true),
                    ticket = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    booking_date_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("c23", x => x.book_id);
                    table.ForeignKey(
                        name: "c24",
                        column: x => x.trip_id,
                        principalTable: "trip",
                        principalColumn: "trip_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "c39",
                        column: x => x.u_id,
                        principalTable: "User",
                        principalColumn: "u_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "bus_trip",
                columns: table => new
                {
                    bus_id = table.Column<int>(type: "int", nullable: false),
                    trip_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    seat_number = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("c102", x => new { x.bus_id, x.trip_id });
                    table.ForeignKey(
                        name: "FK_bus_trip_bus",
                        column: x => x.bus_id,
                        principalTable: "bus",
                        principalColumn: "bus_id");
                    table.ForeignKey(
                        name: "FK_bus_trip_trip",
                        column: x => x.trip_id,
                        principalTable: "trip",
                        principalColumn: "trip_id");
                });

            migrationBuilder.CreateTable(
                name: "t_ticket",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trib_id = table.Column<int>(type: "int", nullable: true),
                    number_ticket = table.Column<int>(type: "int", nullable: false),
                    booked_ticket = table.Column<int>(type: "int", nullable: true),
                    avilable_ticket = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("c21", x => x.ticket_id);
                    table.ForeignKey(
                        name: "c22",
                        column: x => x.trib_id,
                        principalTable: "trip",
                        principalColumn: "trip_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_trip_id",
                table: "Booking",
                column: "trip_id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_u_id",
                table: "Booking",
                column: "u_id");

            migrationBuilder.CreateIndex(
                name: "c101",
                table: "bus_trip",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bus_trip_trip_id",
                table: "bus_trip",
                column: "trip_id");

            migrationBuilder.CreateIndex(
                name: "IX_sp_Booking_u_id",
                table: "sp_Booking",
                column: "u_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_ticket_trib_id",
                table: "t_ticket",
                column: "trib_id");

            migrationBuilder.CreateIndex(
                name: "IX_trip_bus_id",
                table: "trip",
                column: "bus_id");

            migrationBuilder.CreateIndex(
                name: "c14",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "c15",
                table: "User",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "c16",
                table: "User",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "bus_trip");

            migrationBuilder.DropTable(
                name: "sp_Booking");

            migrationBuilder.DropTable(
                name: "t_ticket");

            migrationBuilder.DropTable(
                name: "bus");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "Admin");
        }
    }
}
