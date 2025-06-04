using Microsoft.AspNetCore.Mvc;
using Projekt_sbd.Data;
using Microsoft.AspNetCore.Authorization;

namespace Projekt_sbd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly OracleDbContext _context;

        public RankingController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: /api/ranking
        [HttpGet]
        [Authorize(Roles = "admin,teacher")]
        public IActionResult GetRanking()
        {
            var ranking = _context.StudentRanking.ToList();
            return Ok(ranking);
        }
    }
}
