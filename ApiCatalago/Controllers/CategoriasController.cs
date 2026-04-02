using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Categorias : ControllerBase
    {
        private readonly AppDbContext _context;

        public Categorias(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categorias = _context.Categorias.Include(c => c.Produtos).ToList();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas...");
            }
            return Ok(categorias);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _context.Categorias.ToList();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas...");
            }
            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada...");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Categoria é nula...");
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Id da categoria não corresponde...");
            }
            var categoriaBanco = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoriaBanco is null)
            {
                return NotFound("Categoria não encontrada...");
            }
           
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada...");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
        
}
