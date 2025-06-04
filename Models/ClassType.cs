using Projekt_sbd.Models;

public class ClassType
{
    public int IdClassType { get; set; }
    public string Nazwa { get; set; }

    public ICollection<ClassGroup>? ClassGroups { get; set; }
}
