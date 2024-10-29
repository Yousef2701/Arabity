using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class Favourite
    {
        [ForeignKey("Customer")]
        public string? CustomerId { get; set; } = null;

        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("StoreProduct")]
        public string? ProductParcode { get; set; } = null;

        public virtual StoreProduct StoreProduct { get; set; } = null!;
    }
}
