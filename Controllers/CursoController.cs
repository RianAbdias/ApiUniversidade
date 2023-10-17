using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiUniversidade.Context;
using apiUniversidade.Model;
using Microsoft.AspNetCore.Mvc;

namespace apiUniversidade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CursoController : ControllerBase
    {
    public readonly ILogger<CursoController> _logger;
    public readonly ApiUniversidadeContext _context;
    public CursoController(ILogger<CursoController> logger, ApiUniversidadeContext context)
    {
        _logger = logger;
        _context = context;
    }
    
        [HttpGet]
        public ActionResult<IEnumerable<Curso>> Get()
        {
            var cursos = _context.Cursos.ToList();
            if(cursos is null)
                return NotFound();
            return cursos;
        }

        [HttpPost]
        public ActionResult Post(Curso curso){
            _context.Cursos.Add(curso);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCurso",
            new{ id = curso.Id},
            curso);
        }
    }
}