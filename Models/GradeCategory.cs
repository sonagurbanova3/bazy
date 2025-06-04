using System.ComponentModel.DataAnnotations.Schema;
using Projekt_sbd.Models;

namespace Projekt_sbd.Models
{
    [Table("GRADE_CATEGORIES")]
    public class GradeCategory
    {
        [Column("ID_CATEGORY")]
        public int IdCategory { get; set; }

        [Column("NAZWA")]
        public string Nazwa { get; set; }

        public ICollection<Grade>? Grades { get; set; }
    }
}
