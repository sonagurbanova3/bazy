using Projekt_sbd.Models;

public class GradeHistory
{
    public int IdHistory { get; set; }
    public int IdGrade { get; set; }
    public DateTime DataModyfikacji { get; set; }
    public decimal StaraWartosc { get; set; }
    public decimal NowaWartosc { get; set; }
    public string Uzasadnienie { get; set; }

    public Grade? Grade { get; set; }
}
