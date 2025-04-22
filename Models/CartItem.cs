namespace WebAppTest.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public string? CategoryName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
