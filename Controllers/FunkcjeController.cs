using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_sbd.Data;
using System.Data.Common;
using System.Security.Claims;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class FunkcjeController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public FunkcjeController(OracleDbContext context)
        {
            _context = context;
        }

        // 🔹 Średnia ocen studenta (admin, teacher, student sam dla siebie)
        [HttpGet("srednia-studenta/{id}")]
        [Authorize(Roles = "admin,teacher,student")]
        public IActionResult GetSredniaStudenta(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userLogin = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userRole == "student")
            {
                // znajdź ID zalogowanego studenta
                var user = _context.Users.FirstOrDefault(u => u.Username == userLogin);
                var student = _context.Students.FirstOrDefault(s => s.Email == userLogin);

                if (student == null || student.IdStudent != id)
                    return Forbid("Student może sprawdzić tylko swoją średnią");
            }

            using DbConnection connection = _context.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "BEGIN :result := f_srednia_studenta(:id); END;";

            var resultParam = command.CreateParameter();
            resultParam.ParameterName = "result";
            resultParam.DbType = System.Data.DbType.Decimal;
            resultParam.Direction = System.Data.ParameterDirection.ReturnValue;
            command.Parameters.Add(resultParam);

            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;
            command.Parameters.Add(idParam);

            command.ExecuteNonQuery();
            return Ok(resultParam.Value);
        }

        // 🔹 Oblicz ocenę końcową (tylko admin lub teacher)
        [HttpPost("oblicz-ocene")]
        [Authorize(Roles = "admin,teacher")]
        public IActionResult ObliczOceneKoncowa([FromQuery] int idStudent, [FromQuery] int idSubject)
        {
            using DbConnection connection = _context.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "BEGIN pkg_oceny.ObliczOceneKoncowa(:idStudent, :idSubject); END;";

            var param1 = command.CreateParameter();
            param1.ParameterName = "idStudent";
            param1.Value = idStudent;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "idSubject";
            param2.Value = idSubject;
            command.Parameters.Add(param2);

            command.ExecuteNonQuery();
            return Ok("Ocena końcowa została obliczona.");
        }
    }
}
