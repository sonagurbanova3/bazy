using Projekt_sbd.Models;
using System.ComponentModel.DataAnnotations.Schema;

[Table("STUDENTS")]
public class Student
{
    [Column("ID_STUDENT")]
    public int IdStudent { get; set; }

    [Column("IMIE")]
    public string Imie { get; set; }

    [Column("NAZWISKO")]
    public string Nazwisko { get; set; }

    [Column("NR_INDEKSU")]
    public string NrIndeksu { get; set; }

    [Column("EMAIL")]
    public string Email { get; set; }

    [Column("DATA_URODZ")]
    public DateTime? DataUrodz { get; set; }

    public ICollection<Grade>? Grades { get; set; }
}
