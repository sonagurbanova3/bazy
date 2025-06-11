using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_sbd.Data;
using Projekt_sbd.Models;
using Projekt_sbd.DTOs;
using System.Linq;

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

        // POST: /api/subjects
        [HttpPost]
        [Authorize(Roles = "admin,teacher")]
        public IActionResult AddSubject([FromBody] SubjectDTO subjectDto)
        {
            if (subjectDto == null)
                return BadRequest();

            var subject = new Subject
            {
                Nazwa = subjectDto.Nazwa,
                IdTeacher = subjectDto.IdTeacher,
                // Semestr możesz ustawić domyślnie lub rozbudować DTO jeśli potrzebujesz
            };

            _context.Subjects.Add(subject);
            _context.SaveChanges();

            // Przypisz wygenerowany IdSubject do DTO
            subjectDto.IdSubject = subject.IdSubject;

            return CreatedAtAction(nameof(GetById), new { id = subject.IdSubject }, subjectDto);
        }

        // GET pomocniczy - zwraca SubjectDTO
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return NotFound();

            var subjectDto = new SubjectDTO
            {
                IdSubject = subject.IdSubject,
                Nazwa = subject.Nazwa,
                IdTeacher = subject.IdTeacher
            };

            return Ok(subjectDto);
        }
    }
}
