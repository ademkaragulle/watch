using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        DataContext db = new DataContext();
        [HttpPost]
        public async Task<ActionResult<string>> messages(Contact contact)
        {
            if (contact != null)
            {
                await db.Contact.AddAsync(contact);
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
