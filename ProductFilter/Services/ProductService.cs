using ProductFilter.Models;
using ProductFilter.Repositories;
using System.Globalization;

namespace ProductFilter.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public BaseResponse<IEnumerable<Product>> GetFilteredProducts(Dictionary<string, string> filters)
        {
            try
            {
                var products = _productRepository.GetAll();
                var filteredProducts = FilterProducts(products, filters);
                
                return new BaseResponse<IEnumerable<Product>>
                {
                    Data = filteredProducts,
                    Description = $"Recieved {filteredProducts.Count()} products",
                    StatusCode = StatusCode.Ok
                };                           
            }
            catch (ArgumentException argumentExc)
            {
                return new BaseResponse<IEnumerable<Product>>
                {                  
                    Description = argumentExc.Message,
                    StatusCode = StatusCode.BadRequest
                };
            }
            catch (Exception ex) 
            {
                return new BaseResponse<IEnumerable<Product>>
                {                
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private IEnumerable<Product> FilterProducts(IEnumerable<Product> products, Dictionary<string, string> filters)
        {
            var filteredProducts = products;

            if (filters.Count > 0 || filters.Count <= 3)
            {
                foreach (string filterName in filters.Keys)
                {
                    switch (filterName)
                    {
                        case "name":
                            filteredProducts = FilterByName(filteredProducts, filters[filterName]).ToList();
                            break;
                        case "is_new":
                            filteredProducts = IsNew(filteredProducts, filters[filterName]).ToList();
                            break;
                        case "price":                                                     
                            filteredProducts = FilterByPrice(filteredProducts, filters[filterName]).ToList();
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }

            return filteredProducts;
        }

        private IEnumerable<Product> FilterByName(IEnumerable<Product> products, string subString)
        {
            return products.Where(p => p.Name.Contains(subString, StringComparison.OrdinalIgnoreCase));
        }

        private IEnumerable<Product> IsNew(IEnumerable<Product> products, string state)
        {                      
            if (Convert.ToInt32(state) != 0)
                return products.Where(p => p.IsNew == true);

            return products.Where(p => p.IsNew == false);                     
        }

        private IEnumerable<Product> FilterByPrice(IEnumerable<Product> products, string  str)
        {          
            var startStr = str.Substring(str.IndexOf('(') + 1, str.IndexOf(',') - str.IndexOf('(') - 1);
            decimal start = Convert.ToDecimal(startStr, new CultureInfo("en-US"));

            var endStr = str.Substring(str.IndexOf(',') + 1, str.IndexOf(')') - str.IndexOf(',') - 1);
            decimal end = Convert.ToDecimal(endStr, new CultureInfo("en-US"));

            return products.Where(p => p.Price >= start && p.Price <= end);
        }
    }        
}
