using Microsoft.AspNetCore.Mvc;
using ProductFilter.Services;

namespace ProductFilter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("/products")]
        public IActionResult GetFilteredProducts([FromQuery] Dictionary<string, string> filters)
        {
            var filteredProducts = _productService.FilterProducts(filters);

            return new OkObjectResult(filteredProducts);
        }
    }
}
