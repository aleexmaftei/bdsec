namespace eCommerce.Models.CartVM
{
    public class CartVM
    {
        public int ProductId { get; set; }
        public double ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int QuantityBuy { get; set; }
    }
}
