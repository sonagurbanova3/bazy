using Projekt_sbd.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Grade
{
    [Column("ID_GRADE")]
    public int IdGrade { get; set; }

    [Column("ID_STUDENT")]
    public int IdStudent { get; set; }

    [Column("ID_GROUP")]
    public int IdGroup { get; set; }

    [Column("ID_CATEGORY")]
    public int IdCategory { get; set; }

    [Column("DATA_OCENY")]
    public DateTime DataOceny { get; set; }

    [Column("WARTOSC")]
    public decimal Wartosc { get; set; }

    [Column("KOMENTARZ")]
    public string? Komentarz { get; set; }

    public Student? Student { get; set; }
    public ClassGroup? ClassGroup { get; set; }
    public GradeCategory? GradeCategory { get; set; }
}
