using ProductFilter.Models;

namespace ProductFilter.Services
{
    public interface IProductService
    {
        BaseResponse<IEnumerable<Product>> GetFilteredProducts(Dictionary<string, string> filters);
    }
}
