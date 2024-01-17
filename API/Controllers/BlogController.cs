using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        DataContext db = new DataContext();

        [HttpGet("/blogs")]
        public async Task<ActionResult<List<Blog>>> blogs()
        {
            var blogs = await db.Blog.ToListAsync();
            return blogs;
        }

        [HttpGet("/comments")]
        public async Task<ActionResult<List<BlogComment>>> comments()
        {
            var comments = await db.BlogComment.ToListAsync();
            return comments;
        }

        [HttpPost("/addcomments")]
        public async Task<ActionResult<string>> addcomments(BlogComment blogComment)
        {
            if (blogComment != null)
            {
                blogComment.Date = DateTime.Now;
                await db.BlogComment.AddAsync(blogComment);
                db.SaveChanges();
                return Ok("Ekleme başarılı.");
            }
            else
            {
                return Ok("Ekleme Başarısız.");
            }
        }
    }
}



