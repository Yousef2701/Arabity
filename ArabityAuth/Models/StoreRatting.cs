using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class StoreRatting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Ratting { get; set; }

        public string? StoreId { get; set; } = null;

        public virtual Store Store { get; set; } = null!;
    }
}
