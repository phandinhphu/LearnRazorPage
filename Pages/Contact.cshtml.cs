using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Models;

namespace WebAppTest.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public Contact Contact { get; set; }

        public ContactModel()
        {
            Contact = new Contact();
        }

        public string ThongBao { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                ThongBao = "Dữ liệu hợp lệ";
            }
            else
            {
                ThongBao = "Dữ liệu không hợp lệ";
            }
        }
    }
}
