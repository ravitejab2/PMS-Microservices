using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.PatientAPI.Migrations
{
    public partial class update_Diagnosis_Medication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Drug_Strength",
                table: "Patient_Diagnosis_Medication",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Drug_Strength",
                table: "Patient_Diagnosis_Medication");
        }
    }
}
