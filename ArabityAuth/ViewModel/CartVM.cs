namespace ArabityAuth.ViewModel
{
    public class CartVM
    {
        public string ProductParcode { get; set; }

        public string Name { get; set; } = null!;

        public int ProductQuantity { get; set; }

        public double Price { get; set; }

        public double TotalPrice { get; set; }
    }
}
