using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.PatientAPI.Migrations
{
    public partial class Diagnosis_Medication_Table_Creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient_Diagnosis_Medication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: true),
                    Diagnosis_Code = table.Column<int>(type: "int", nullable: false),
                    Diagnosis_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis_Is_Depricated = table.Column<bool>(type: "bit", nullable: false),
                    Procedure_Code = table.Column<int>(type: "int", nullable: false),
                    Procedure_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procedure_Is_Depricated = table.Column<bool>(type: "bit", nullable: false),
                    Drug_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drug_GenericName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drug_Form = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Diagnosis_Medication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Diagnosis_Medication_PatientVisitVitals_VisitId",
                        column: x => x.VisitId,
                        principalTable: "PatientVisitVitals",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Diagnosis_Medication_VisitId",
                table: "Patient_Diagnosis_Medication",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient_Diagnosis_Medication");
        }
    }
}
