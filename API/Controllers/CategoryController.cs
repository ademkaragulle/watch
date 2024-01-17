using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        DataContext db = new DataContext();

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Categories()
        {
            var categories = await db.Category.ToListAsync();
            return categories;
        }
    }
}
