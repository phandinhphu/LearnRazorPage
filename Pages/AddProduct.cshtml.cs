using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using WebAppTest.Models;
using WebAppTest.Services;

namespace WebAppTest.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly ProductServices _productServices;

        public AddProductModel(ProductServices productServices)
        {
            _productServices = productServices;
        }

        public void OnGet()
        {
        }

        public void OnPost(string id, string name, string des, decimal price)
        {
            try
            {
                Models.Product product = new Models.Product()
                {
                    Id = Int32.Parse(id),
                    Name = name,
                    Description = des,
                    Image = "https://picsum.photos/200",
                    Price = price
                };

                _productServices.AddProduct(product);

                TempData["Message"] = "Product added successfully";
                TempData["Type"] = "success";
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
