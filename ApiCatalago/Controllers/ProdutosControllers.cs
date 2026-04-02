using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Produtos : ControllerBase
    {

        private readonly AppDbContext _context;

        public Produtos(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos =_context.Produtos.ToList();
            if(produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new {id = produto.ProdutoId}, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

    }
}
