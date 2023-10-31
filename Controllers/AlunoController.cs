using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using apiUniversidade.Context;
using apiUniversidade.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace apiUniversidade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
    public readonly ILogger<AlunoController> _logger;
    public readonly ApiUniversidadeContext _context;
    public AlunoController(ILogger<AlunoController> logger, ApiUniversidadeContext context)
    {
        _logger = logger;
        _context = context;
    } 
        [HttpGet(Name = "alunos")]
        public List<Aluno> GetAluno()
        {
            List<Aluno> alunos = new List<Aluno>();

            Aluno a1 = new Aluno();
            a1.Nome = "Amanda";
            a1.DataNascimento = DateTime.Now;
            a1.cpf = "123.456.789-52";

            Aluno a2 = new Aluno();
            a2.Nome = "Camila";
            a2.DataNascimento = DateTime.Now;
            a2.cpf = "987.654.321-85";

            alunos.Add(a1);
            alunos.Add(a2);

            return alunos;

        }

        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> Get()
        {
            var alunos = _context.Alunos.ToList();
            if(alunos is null)
                return NotFound();
            return alunos;
        }

        [HttpGet("(id:int)", Name ="GetAluno")]
        public ActionResult<Aluno> Get(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(p => p.Id == id);
            if(aluno is null)
                return NotFound("Aluno não encontrado");
            return aluno;
        }

        [HttpPost]
        public ActionResult Post(Aluno aluno){
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetAluno",
            new{ id = aluno.Id},
            aluno);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Aluno aluno){
            if(id != aluno.Id)
            return BadRequest();

            _context.Entry(aluno).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(aluno);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var aluno = _context.Alunos.FirstOrDefault(p => p.Id == id);
            if(aluno is null)
                return NotFound();
            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }
    }
}