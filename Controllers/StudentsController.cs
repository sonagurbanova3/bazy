using Microsoft.AspNetCore.Mvc;
using Projekt_sbd.Data;
using Projekt_sbd.Models;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentsController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public StudentsController(OracleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Students.ToList());
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
    }
}
