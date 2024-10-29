using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class WorkshopComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; } = null!;

        public string? WorkshopId { get; set; } = null;

        public virtual Workshop Workshop { get; set; } = null!;
    }
}
