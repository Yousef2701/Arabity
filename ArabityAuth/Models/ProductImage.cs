using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class ProductImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string? StoreProductParcode { get; set; } = null;

        public virtual StoreProduct StoreProduct { get; set; } = null!;
    }
}
