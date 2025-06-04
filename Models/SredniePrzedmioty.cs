using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[Keyless]
[Table("V_SREDNIE_PRZEDMIOTY")]
public class SredniePrzedmioty
{
    [Column("PRZEDMIOT")]
    public string Przedmiot { get; set; }

    [Column("TYP_ZAJEC")]
    public string TypZajec { get; set; }

    [Column("SREDNIA")]
    public decimal Srednia { get; set; }
}
