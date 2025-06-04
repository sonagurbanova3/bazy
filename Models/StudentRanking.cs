using System.ComponentModel.DataAnnotations.Schema;
using Projekt_sbd.Models;

namespace Projekt_sbd.Models
{
    [Table("V_RANKING_STUDENTOW")]
    public class StudentRanking
    {
        [Column("ID_STUDENT")]
        public int IdStudent { get; set; }

        [Column("STUDENT")]
        public string Student { get; set; }

        [Column("SREDNIA_OCEN")]
        public decimal SredniaOcen { get; set; }
    }
}
