using Microsoft.EntityFrameworkCore.Migrations;



namespace PMS.PatientAPI.Migrations
{
    public partial class Update_Table_Patient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "PatientEmergencyContacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PatientDemographics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PatientDemographics");
        }
    }
}
