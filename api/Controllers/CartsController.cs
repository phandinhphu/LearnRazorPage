using Microsoft.AspNetCore.Mvc;
using WebAppTest.Models;
using WebAppTest.Extensions;

namespace WebAppTest.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase // Fix CS0120 by inheriting from ControllerBase
    {
        private readonly ILogger<CartsController> _logger;

        public CartsController(ILogger<CartsController> logger)
        {
            _logger = logger;
        }

        // GET api/Carts
        [HttpGet]
        public IActionResult GetCart()
        {
            // Fix CS0120: Access HttpContext.Session via the inherited ControllerBase property
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            return Ok(cart);
        }

        // DELETE api/Carts/1
        [HttpDelete("{productId}")]
        public IActionResult DeleteCart(int productId)
        {
            // Fix CS0120: Access HttpContext.Session via the inherited ControllerBase property
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return Ok(cart);
        }
    }
}
