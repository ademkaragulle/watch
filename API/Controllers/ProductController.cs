using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        DataContext db=new DataContext();
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() 
        { 
            var products=await db.Product.ToListAsync();
            return products;
        }
    }
}
