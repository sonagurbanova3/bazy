using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_sbd.Data;
using Projekt_sbd.Models;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public SubjectsController(OracleDbContext context)
        {
            _context = context;
        }

     

        /// POST: /api/subjects
        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public IActionResult AddSubject([FromBody] Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = subject.IdSubject }, subject);
        }

        // GET pomocniczy, jeśli nie masz
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return NotFound();
            return Ok(subject);
        }



       
    }
}
