using ApiCatalago.Context;
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
    }
}
