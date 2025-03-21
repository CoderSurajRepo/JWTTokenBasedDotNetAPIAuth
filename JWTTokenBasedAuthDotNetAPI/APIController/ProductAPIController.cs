using JWTTokenBasedAuthDotNetAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTTokenBasedAuthDotNetAPI.APIController
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        [HttpGet("GetAllProductList")]
        public IActionResult GetAllProductList()
        {
            return Ok(new List<ProductModel> { new ProductModel { ProductID = 1, ProductName = "TP Link Router" },
            new ProductModel { ProductID = 2, ProductName = "LG Monitor" },
            new ProductModel { ProductID = 3, ProductName = "LG Mouse" }});
        }
    }
}
