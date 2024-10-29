using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class StoreCarType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string CarType { get; set; } = null!;

        public string? StoreId { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;
    }
}
