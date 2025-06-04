using Projekt_sbd.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ASSESSMENTS")]
public class Assessment
{
    [Key]
    [Column("ID_ASSESSMENT")]
    public int IdAssessment { get; set; }

    [Column("ID_GROUP")]
    public int IdGroup { get; set; }

    [Column("ID_CATEGORY")]
    public int IdCategory { get; set; }

    [Column("DATA_OCENY")]
    public DateTime DataOceny { get; set; }

    [Column("OPIS")]
    public string Opis { get; set; }

    // Relacje jeśli masz (opcjonalnie)
    public ClassGroup? Group { get; set; }
    public GradeCategory? Category { get; set; }
}
