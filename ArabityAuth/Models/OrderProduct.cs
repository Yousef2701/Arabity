using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class OrderProduct
    {
        [ForeignKey("Order")]
        public int OrderNumbre { get; set; }

        [ForeignKey("StoreProduct")]
        public string ProductParcode { get; set; }

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public double TotalQuantityPrice { get; set; }

        public virtual StoreProduct StoreProduct { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;
    }
}
