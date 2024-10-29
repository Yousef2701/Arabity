using System.ComponentModel.DataAnnotations;

namespace ArabityAuth.Models
{
    public class Order
    {
        [Key]
        public int OrderNumbre { get; set; }

        public double TotalPrice { get; set; }

        public string OrderDate { get; set; }

        public string OrderStatus { get; set; }

        public string clientName { get; set; }

        public string clientPhone { get; set; }

        public string clientGmail { get; set; }

        public string clientGovernorate { get; set; }

        public string clientCity { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();

        public virtual ICollection<OrderStore> OrderStores { get; } = new List<OrderStore>();
    }
}
