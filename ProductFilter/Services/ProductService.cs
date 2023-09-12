using ProductFilter.Models;
using ProductFilter.Repositories;

namespace ProductFilter.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> FilterProducts(Dictionary<string, string> filters)
        {
            try
            {
                var filteredProducts = _productRepository.GetAll();

                if(filters.Count > 0 || filters.Count <= 3)
                {
                    foreach (string filterName  in filters.Keys)
                    {
                        switch (filterName)
                        {
                            case "name":
                                filteredProducts = FilterByName(filteredProducts, filters[filterName]);
                            break;
                            case "is_new":
                                filteredProducts = IsNew(filteredProducts);
                            break;
                            default:
                                throw new ArgumentException();
                        }
                    }

                    return filteredProducts.ToList();
                }
                else
                {
                    return null; // TODO: Throw some exceptions
                }                 
            }
            catch (Exception ex) 
            {
                return null; // TODO: Handle exception
            }
        }

        private IEnumerable<Product> FilterByName(IEnumerable<Product> products, string name)
        {
           return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));           
        }

        private IEnumerable<Product> IsNew(IEnumerable<Product> products)
        {
            return products.Where(p => p.IsNew == true);
        }

        private IEnumerable<Product> FilterByPrice(IEnumerable<Product> products, int start, int end)
        {
            return products.Where(p => p.Price >= start && p.Price <= end);
        }
    }
}
