using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class ProductRatting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Ratting { get; set; }

        public string? StoreProductParcode { get; set; } = null;

        public virtual StoreProduct StoreProduct { get; set; } = null!;
    }
}
