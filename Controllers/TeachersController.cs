using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Projekt_sbd.Data;
using Projekt_sbd.Models;
using System.Linq;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public TeachersController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: /api/teachers
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetTeachers()
        {
            var teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }
    }
}
