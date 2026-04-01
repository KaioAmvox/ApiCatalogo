using ApiCatalago.Context;
using ApiCatalago.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosControllers : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProdutosControllers(AppDbContext context)
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
    }
}
