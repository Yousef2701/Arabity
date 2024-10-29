using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class WorkshopRatting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Ratting { get; set; }

        public string? WorkshopId { get; set; } = null;

        public virtual Workshop Workshop { get; set; } = null!;
    }
}
