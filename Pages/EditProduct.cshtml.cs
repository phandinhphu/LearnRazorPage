using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Models;
using WebAppTest.Services;

namespace WebAppTest.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly ProductServices _productServices;
        public int Id { get; set; }

        public EditProductModel(ProductServices productServices)
        {
            _productServices = productServices;
        }

        public void OnGet(int id)
        {
            Id = id;
        }

        public void OnPost(string id, string name, string des, decimal price)
        {
            try
            {
                _productServices.UpdateProduct(new Product()
                {
                    Id = Int32.Parse(id),
                    Name = name,
                    Description = des,
                    Image = "https://picsum.photos/200",
                    Price = price
                });

                Response.Redirect("/Products");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
