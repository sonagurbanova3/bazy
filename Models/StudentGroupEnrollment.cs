using Projekt_sbd.Models;

public class StudentGroupEnrollment
{
    public int IdStudent { get; set; }
    public int IdGroup { get; set; }

    public Student? Student { get; set; }
    public ClassGroup? Group { get; set; }
}
