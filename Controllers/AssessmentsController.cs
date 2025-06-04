using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_sbd.Data;
using Projekt_sbd.Models;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentsController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public AssessmentsController(OracleDbContext context)
        {
            _context = context;
        }

        // 🔒 tylko nauczyciel lub admin mogą przeglądać
        [HttpGet]
        [Authorize(Roles = "teacher,admin")]
        public IActionResult GetAllAssessments()
        {
            var assessments = _context.Assessments
                .Include(a => a.Group)
                .Include(a => a.Category)
                .ToList();
            return Ok(assessments);
        }

        // 🔒 tylko admin może dodać formę oceniania
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddAssessment([FromBody] Assessment assessment)
        {
            _context.Assessments.Add(assessment);
            _context.SaveChanges();
            return Ok(assessment);
        }
    }
}
