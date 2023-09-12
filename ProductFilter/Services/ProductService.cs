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
                foreach(string filterName in filters.Keys) 
                {
                    switch (filterName)
                    {
                        case "name":
                           return FilterByName(filters[filterName]);
                        case "is_new":
                            return IsNew();
                        case "price":

                    }
                    return null;
                }
                return null;
            }
            catch 
            {
                return null;
            }
        }

        private IEnumerable<Product> FilterByName(string name)
        {
            return _productRepository.GetAll().Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private IEnumerable<Product> IsNew()
        {
            return _productRepository.GetAll().Where(p => p.IsNew == true).ToList();
        }

        private IEnumerable<Product> FilterByPrice(int start, int end)
        {
            return _productRepository.GetAll().Where(p => p.Price >= start && p.Price <= end).ToList();
        }
    }
}
