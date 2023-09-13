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
            var response = _productService.GetFilteredProducts(filters);

            if (response.StatusCode == Models.StatusCode.Ok)
                return new OkObjectResult(response);
            else if (response.StatusCode == Models.StatusCode.BadRequest)
                return new BadRequestResult();
            else
                return new BadRequestObjectResult(response);         
        }
    }
}
