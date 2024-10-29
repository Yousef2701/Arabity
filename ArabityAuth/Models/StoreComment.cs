using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArabityAuth.Models
{
    public class StoreComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; } = null!;

        public string? StoreId { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;
    }
}
