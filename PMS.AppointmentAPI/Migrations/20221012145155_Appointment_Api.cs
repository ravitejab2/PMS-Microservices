using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.AppointmentAPI.Migrations
{
    public partial class Appointment_Api : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Meetingtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicianId = table.Column<int>(type: "int", nullable: false),
                    Appointmentdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentStartdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppointmentEnddate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SlotId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    SlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlotTiming = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlotStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    SlotEnd = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.SlotId);
                });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "SlotId", "SlotEnd", "SlotStart", "SlotTiming" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "10AM - 11AM" },
                    { 2, new TimeSpan(0, 12, 0, 0, 0), new TimeSpan(0, 11, 0, 0, 0), "11 AM - 12 PM" },
                    { 3, new TimeSpan(0, 13, 0, 0, 0), new TimeSpan(0, 12, 0, 0, 0), "12 PM - 1 PM" },
                    { 4, new TimeSpan(0, 14, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), "1 PM - 2PM" },
                    { 5, new TimeSpan(0, 15, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0), "2 PM - 3 PM" },
                    { 6, new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0), "3 PM - 4 PM" },
                    { 7, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 16, 0, 0, 0), "4 PM - 5 PM" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Slots");
        }
    }
}
