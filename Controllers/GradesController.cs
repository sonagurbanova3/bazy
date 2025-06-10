using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_sbd.Data;
using Projekt_sbd.Models;
using System.Data.Common;
using System.Security.Claims;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public GradesController(OracleDbContext context)
        {
            _context = context;
        }

        // ✅ GET: /api/grades – tylko admin i nauczyciel
        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public IActionResult GetAllGrades()
        {
            var grades = _context.Grades
                .Include(g => g.Student)
                .Include(g => g.ClassGroup)
                .Include(g => g.GradeCategory)
                .ToList();
            return Ok(grades);
        }

       

        // ✅ POST: /api/grades – tylko nauczyciel
        [HttpPost]
        [Authorize(Roles = "teacher")]
        public IActionResult AddGrade([FromBody] Grade grade)
        {
            grade.DataOceny = DateTime.Now;
            _context.Grades.Add(grade);
            _context.SaveChanges();
            return Ok(grade);
        }

        // ✅ POST: /api/grades/plsql – tylko nauczyciel
        [HttpPost("plsql")]
        [Authorize(Roles = "teacher")]
        public IActionResult AddGradeViaProcedure([FromBody] Grade grade)
        {
            using DbConnection connection = _context.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "BEGIN DodajOcene(:p1, :p2, :p3, :p4, :p5); END;";

            var p1 = command.CreateParameter();
            p1.ParameterName = "p1";
            p1.Value = grade.IdStudent;
            command.Parameters.Add(p1);

            var p2 = command.CreateParameter();
            p2.ParameterName = "p2";
            p2.Value = grade.IdGroup;
            command.Parameters.Add(p2);

            var p3 = command.CreateParameter();
            p3.ParameterName = "p3";
            p3.Value = grade.IdCategory;
            command.Parameters.Add(p3);

            var p4 = command.CreateParameter();
            p4.ParameterName = "p4";
            p4.Value = grade.Wartosc;
            command.Parameters.Add(p4);

            var p5 = command.CreateParameter();
            p5.ParameterName = "p5";
            p5.Value = grade.Komentarz;
            command.Parameters.Add(p5);

            command.ExecuteNonQuery();

            return Ok("Dodano ocenę przez procedurę PL/SQL.");
        }
    }
}
