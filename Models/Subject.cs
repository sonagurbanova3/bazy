using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_sbd.Models
{
    [Table("SUBJECTS")]
    public class Subject
    {
        [Key] // ⬅️ TO DODAJ
        [Column("ID_SUBJECT")]
        public int IdSubject { get; set; }

        [Column("NAZWA")]
        public string Nazwa { get; set; }

        [Column("SEMESTR")]
        public int Semestr { get; set; }

        [Column("ID_TEACHER")]
        public int IdTeacher { get; set; }

        public Teacher? Teacher { get; set; }
        public ICollection<ClassGroup>? ClassGroups { get; set; }
        public ICollection<StudentResult>? StudentResults { get; set; }
        public ICollection<AssessmentRule>? AssessmentRules { get; set; }
    }
}
