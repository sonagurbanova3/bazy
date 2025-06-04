using Projekt_sbd.Models;

public class StudentResult
{
    public int IdStudent { get; set; }
    public int IdSubject { get; set; }
    public decimal OcenaKoncowa { get; set; }
    public string Zaliczone { get; set; }
    public DateTime DataWyliczenia { get; set; }

    public Student? Student { get; set; }
    public Subject? Subject { get; set; }
}
