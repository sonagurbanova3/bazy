using Microsoft.AspNetCore.Mvc;
using Projekt_sbd.Data;

[ApiController]
[Route("api/[controller]")]

public class SrednieController : ControllerBase
{
    private readonly OracleDbContext _context;

    public SrednieController(OracleDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetSrednie()
    {
        var srednie = _context.SredniePrzedmioty.ToList();
        return Ok(srednie);
    }
}
