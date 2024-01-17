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
    public class OrderController : ControllerBase
    {

        DataContext db = new DataContext();



        [HttpGet("/getOrder/{username}")]
        [Authorize]
        public async Task<ActionResult<List<OrderResponse>>> GetOrder(string username)
        {
            List<OrderResponse> orderResponseList = new List<OrderResponse>();

            // Kullanıcı kontrolü
            var user = db.User.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            List<Order> orderList = db.Order.Where(x => x.UserId == user.Id).ToList();
            OrderDetail orderDetail1;
            foreach (var order in orderList)
            {
                var orderDetails = db.OrderDetail
                    .Where(x => x.OrderId == order.Id)
                    .ToList();
                List<ProductViewModel> productList = new List<ProductViewModel>();
                foreach (var orderDetail in orderDetails)
                {
                    var product = db.Product.FirstOrDefault(x => x.Id == orderDetail.ProductId);
                    if (product != null)
                    {
                        productList.Add(new ProductViewModel { Product = product, Quantity = orderDetail.Quantity });
                    }
                }

                //Her sipariş için ayrı OrderResponse nesnesi oluşturun
                OrderResponse orderResponse = new OrderResponse
                {
                    OrderList = productList,
                    OrderId = order.Id,
                    OrderDate = order.Date
                };

                orderResponseList.Add(orderResponse);
            }

            return orderResponseList;
        }



        [HttpPost("/postOrder")]
        [Authorize]

        public async Task<ActionResult<string>> postOrderAsync([FromBody] OrderRequest orderRequest)
        {
            var result = "Başarısız";
            var user = db.User.FirstOrDefault(x => x.Username == orderRequest.Username);
            var userId = user.Id;

            Order newOrder = new Order();
            if (userId != null)
            {
                newOrder.UserId = userId;
                newOrder.Name = orderRequest.Name;
                newOrder.Surname = orderRequest.Surname;
                newOrder.Address1 = orderRequest.Address1;
                newOrder.Address2 = orderRequest.Address2;
                newOrder.Town = orderRequest.Town;
                newOrder.State = orderRequest.State;
                newOrder.PostCode = orderRequest.PostCode;
                newOrder.Phone = orderRequest.Phone;
                newOrder.Email = orderRequest.Email;
                newOrder.Date = DateTime.Now;
                await db.Order.AddAsync(newOrder);
                db.SaveChanges();
                foreach (var item in orderRequest.Orders)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = newOrder.Id;
                    orderDetail.ProductId = item.Id;
                    orderDetail.Quantity = item.Quantity;
                    await db.OrderDetail.AddAsync(orderDetail);
                    db.SaveChanges();
                }
                result = "Başarılı";
            }

            return result;
        }
    }
}
