using ProductFilter.Models;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace ProductFilter.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name= "Apple",
                Description = "The best apples in the UK",
                Price = 12.64m,
                IsNew = true,
                AvailableFrom = DateTime.ParseExact("2012-04-23T18:25:43.511Z", "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal)
            },
            new Product
            {
                Id = 2,
                Name= "Pineapple",
                Description = "The best pineapples in the USA",
                Price = 38.2m,
                IsNew = false,
                AvailableFrom = DateTime.ParseExact("2022-10-22T04:47:00.511Z", "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal)
            },
            new Product
            {
                Id = 3,
                Name= "Onion",
                Description = "This onion won’t make you cry",
                Price = 0.99m,
                IsNew = true,
                AvailableFrom = DateTime.ParseExact("2000-06-19T08:57:39.000Z", "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal)
            },
            new Product
            {
                Id = 4,
                Name= "Cucumber",
                Description = "The greenest cucumber ever",
                Price = 0.49m,
                IsNew = false,
                AvailableFrom = DateTime.ParseExact("2024-04-23T18:25:43.511Z", "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal)
            },
            new Product
            {
                Id = 5,
                Name= "Mushrooms",
                Description = "“High, high, high",
                Price = 78.25m,
                IsNew = true,
                AvailableFrom = DateTime.ParseExact("2030-04-23T18:25:43.511Z", "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal)
            }
        };

        public Task Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Task Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
