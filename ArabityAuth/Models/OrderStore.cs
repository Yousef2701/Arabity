using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class OrderStore
    {
        [ForeignKey("Order")]
        public int OrderNumbre { get; set; }

        [ForeignKey("Store")]
        public string StoreId { get; set; }

        public double TotalPrice { get; set; }

        public string OrderStatus { get; set; }

        public string ClintName { get; set; }

        public string OrderDate { get; set; }

        public virtual Store Store { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;

    }
}
