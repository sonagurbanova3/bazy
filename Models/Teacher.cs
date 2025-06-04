using Projekt_sbd.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TEACHERS")]
public class Teacher
{
    [Key] // ⬅️ TO DODAJ
    [Column("ID_TEACHER")]
    public int IdTeacher { get; set; }

    [Column("IMIE")]
    public string Imie { get; set; }

    [Column("NAZWISKO")]
    public string Nazwisko { get; set; }

    [Column("EMAIL")]
    public string Email { get; set; }

    public ICollection<Subject>? Subjects { get; set; }
}
