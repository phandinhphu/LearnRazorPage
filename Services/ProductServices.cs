using WebAppTest.Models;

namespace WebAppTest.Services
{
    public class ProductServices
    {
        private List<Product> products = new List<Product>
        {
            new Product {
                Id = 1,
                Name = "Samsung Galaxy S21",
                Description = "Samsung Galaxy S21 5G 128GB Phantom Gray",
                Image = "https://picsum.photos/200",
                Price = 1000
            },
            new Product {
                Id = 2,
                Name = "Samsung Galaxy S20",
                Description = "Samsung Galaxy S20 5G 128GB Cosmic Gray",
                Image = "https://picsum.photos/200",
                Price = 900
            },
            new Product {
                Id = 3,
                Name = "Samsung Galaxy S10",
                Description = "Samsung Galaxy S10 128GB Prism Black",
                Image = "https://picsum.photos/200",
                Price = 800
            },
            new Product {
                Id = 4,
                Name = "Samsung Galaxy S9",
                Description = "Samsung Galaxy S9 64GB Midnight Black",
                Image = "https://picsum.photos/200",
                Price = 700
            },
            new Product {
                Id = 5,
                Name = "Samsung Galaxy S8",
                Description = "Samsung Galaxy S8 64GB Midnight Black",
                Image = "https://picsum.photos/200",
                Price = 600
            }
        };
        private List<Product> tempProduts = new List<Product>();

        public IEnumerable<Product> GetProducts(string queryString = "")
        {
            if (!string.IsNullOrEmpty(queryString))
            {
                return products.Where(p => p.Name.Contains(queryString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return products;
        }

        public Boolean AddProduct(Product product)
        {
            if (product == null)
            {
                return false;
            }
            products.Add(product);
            return true;
        }

        public Boolean UpdateProduct(Product product)
        {
            if (product == null)
            {
                return false;
            }
            var index = products.FindIndex(p => p.Id == product.Id);
            if (index == -1)
            {
                return false;
            }
            products[index] = product;
            return true;
        }

        public Boolean DeleteProduct(int id)
        {
            var index = products.FindIndex(p => p.Id == id);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            return true;
        }

        public Product? GetLastProduct()
        {
            return products.LastOrDefault();
        }

        public void ClearProducts(int[] id)
        {
            var products = this.products.Where(p => id.Contains(p.Id)).ToList();

            tempProduts.AddRange(products);

            foreach (var product in products)
            {
                this.products.Remove(product);
            }
        }

        public void RestoreProducts()
        {
            foreach (var product in tempProduts)
            {
                products.Add(product);
            }
            products = products.OrderBy(p => p.Id).ToList();
            tempProduts.Clear();
        }
    }
}
