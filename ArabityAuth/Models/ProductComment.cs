using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class ProductComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; } = null!;

        public string? StoreProductParcode { get; set; } = null;

        public virtual StoreProduct StoreProduct { get; set; } = null!;
    }
}
