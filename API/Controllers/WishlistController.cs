using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        DataContext db = new DataContext();

        [HttpPost("/wishlist/{username}")]
        [Authorize]
        public async Task<ActionResult<List<Product>>> WishlistAsync(string username)
        {
            var user = db.User.FirstOrDefault(x => x.Username == username);
            if (user != null)
            {
                var userId = user.Id;
                var wishlist = db.Favorite.Where(x => x.UserId == userId).Select(x => x.ProductId).ToList();
                List<Product> productList = new List<Product>();
                foreach (var item in wishlist)
                {
                    var pro = db.Product.FirstOrDefault(x => x.Id == item);
                    productList.Add(pro);
                }
                return productList;
            }
            return NotFound("Kullanıcı bulunamadı");
        }

        [HttpPut("/addWishlist")]
        [Authorize]
        public async Task<ActionResult<string>> AddToWishlistAsync([FromBody] WishlistRequest wishlistRequest)
        {
            var result = "";
            var user = db.User.FirstOrDefault(x => x.Username == wishlistRequest.username);
            var userId = user.Id;
            var IsExist = db.Favorite.Where(x => x.ProductId == wishlistRequest.productId).FirstOrDefault(x => x.UserId == userId);
            if (IsExist != null)
            {
                db.Favorite.Remove(IsExist);
                db.SaveChanges();
                result = "Favorilerden çıkarıldı";
            }
            else
            {
                Favorite favorite = new Favorite();
                favorite.UserId = userId;
                favorite.ProductId = wishlistRequest.productId;
                db.Favorite.Add(favorite);
                db.SaveChanges();
                result = "Favorilere Eklendi";
            }
            return result;
        }



        [HttpDelete("/removeToWishlist")]
        [Authorize]

        public async Task<ActionResult<string>> DeleteToWishlist([FromBody] WishlistRequest wishlistRequest)
        {
            var result = "Favorilerden çıkarılamadı";
            var user = await db.User.FirstOrDefaultAsync(x => x.Username == wishlistRequest.username);
            var userId = user.Id;
            var IsExist = await db.Favorite.Where(x => x.ProductId == wishlistRequest.productId).FirstOrDefaultAsync();
            if (IsExist != null)
            {
                db.Favorite.Remove(IsExist);
                db.SaveChanges();
                result = "Favorilerden çıkarıldı";
            }
            return result;
        }
    }
}
