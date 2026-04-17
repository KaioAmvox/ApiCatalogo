using ApiCatalago.Context;
using ApiCatalago.Filters;
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
        private readonly ILogger<Categorias> _logger;

        public Categorias(AppDbContext context, ILogger<Categorias> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            _logger.LogInformation("============== GET Api/categorias/produtos ================");

            var categorias = _context.Categorias.Include(c => c.Produtos).ToList();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas...");
            }
            return Ok(categorias);
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiloggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            try
            {
                _logger.LogInformation("============== GET Api/categorias ================");
                var categorias = _context.Categorias.AsNoTracking().ToList();
                if (categorias is null)
                {
                    return NotFound($"Categria não encontrada...");
                }

                return Ok(categorias);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao processar a solicitação...");
            }

        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            _logger.LogInformation($"============== GET Api/categorias/id = {id} ================");

            if (categoria is null)
            {
                _logger.LogInformation($"============== GET Api/categorias/id = {id} NOT FOUND ================");
                return NotFound($"Categoria id={id} não encontrada...");
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
                _logger.LogWarning($"Categoria id={id} não encontrada para exclusão...");
                return NotFound("Categoria não encontrada...");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }

}
