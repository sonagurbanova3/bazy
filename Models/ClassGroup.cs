using System.ComponentModel.DataAnnotations.Schema;
using Projekt_sbd.Models;

[Table("CLASS_GROUPS")]
public class ClassGroup
{
    [Column("ID_GROUP")]
    public int IdGroup { get; set; }

    [Column("ID_SUBJECT")]
    public int IdSubject { get; set; }

    [Column("ID_CLASS_TYPE")]
    public int IdClassType { get; set; }

    [Column("GRUPA_KOD")]
    public string GrupaKod { get; set; }

    [Column("ID_TEACHER")]
    public int IdTeacher { get; set; }

    public Subject? Subject { get; set; }
    public ClassType? ClassType { get; set; }
    public Teacher? Teacher { get; set; }

    public ICollection<Grade>? Grades { get; set; }
}
