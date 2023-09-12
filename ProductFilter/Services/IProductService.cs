using ProductFilter.Models;

namespace ProductFilter.Services
{
    public interface IProductService
    {
        IEnumerable<Product> FilterProducts(Dictionary<string, string> filters);
    }
}
