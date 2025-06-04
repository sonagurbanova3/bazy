using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_sbd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    IdStudent = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Imie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Nazwisko = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NrIndeksu = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataUrodz = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.IdStudent);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    IdNauczyciel = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Imie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Nazwisko = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.IdNauczyciel);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    IdPrzedmiot = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nazwa = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Semestr = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdNauczyciel = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.IdPrzedmiot);
                    table.ForeignKey(
                        name: "FK_subjects_teachers_IdNauczyciel",
                        column: x => x.IdNauczyciel,
                        principalTable: "teachers",
                        principalColumn: "IdNauczyciel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    IdOcena = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdStudent = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdPrzedmiot = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataOceny = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Wartosc = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Komentarz = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.IdOcena);
                    table.ForeignKey(
                        name: "FK_grades_students_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "students",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_grades_subjects_IdPrzedmiot",
                        column: x => x.IdPrzedmiot,
                        principalTable: "subjects",
                        principalColumn: "IdPrzedmiot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_grades_IdPrzedmiot",
                table: "grades",
                column: "IdPrzedmiot");

            migrationBuilder.CreateIndex(
                name: "IX_grades_IdStudent",
                table: "grades",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_IdNauczyciel",
                table: "subjects",
                column: "IdNauczyciel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "teachers");
        }
    }
}
