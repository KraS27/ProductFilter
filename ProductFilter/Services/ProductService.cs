using Microsoft.Extensions.Caching.Distributed;
using ProductFilter.Models;
using ProductFilter.Repositories;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProductFilter.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IDistributedCache _cache;

        public ProductService(IBaseRepository<Product> productRepository, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
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
            //var cashKey = JsonSerializer.Serialize(filters);
            //var cashData = _cache.GetString(cashKey);

            //if(!string.IsNullOrEmpty(cashData))            
            //    return JsonSerializer.Deserialize<IEnumerable<Product>>(cashData);
            
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
                            throw new ArgumentException(nameof(filterName));
                    }
                }
            }

            //var cacheOptions = new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            //};

            //_cache.SetString(cashKey, JsonSerializer.Serialize(filteredProducts), cacheOptions);

            return filteredProducts;
        }

        private static IEnumerable<Product> FilterByName(IEnumerable<Product> products, string subString)
        {
            return products.Where(p => p.Name.Contains(subString, StringComparison.OrdinalIgnoreCase));
        }

        private static IEnumerable<Product> IsNew(IEnumerable<Product> products, string state)
        {
            return Convert.ToInt32(state) != 0 ? products.Where(p => p.IsNew == true) : products.Where(p => p.IsNew == false);
        }

        private static IEnumerable<Product> FilterByPrice(IEnumerable<Product> products, string  str)
        {
            Regex regex = new Regex(@"range[(](\d*.\d*),(\d*)[)]");
            decimal start = 0;
            decimal end = 0;

            foreach(Match match in regex.Matches(str))
            {
                start = Convert.ToDecimal(match.Groups[1].Value, new CultureInfo("en-US"));
                end = Convert.ToDecimal(match.Groups[2].Value, new CultureInfo("en-US"));
            }
           
            return products.Where(p => p.Price >= start && p.Price <= end);
        }
    }        
}
