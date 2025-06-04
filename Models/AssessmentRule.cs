using System.ComponentModel.DataAnnotations.Schema;
using Projekt_sbd.Models;

namespace Projekt_sbd.Models
{
    public class AssessmentRule
    {
        public int IdRule { get; set; }

        public int IdSubject { get; set; }
        public int IdClassType { get; set; }

        public decimal Waga { get; set; }
        public decimal ProgZaliczenia { get; set; }

        public Subject? Subject { get; set; }
        public ClassType? ClassType { get; set; }
    }
}
