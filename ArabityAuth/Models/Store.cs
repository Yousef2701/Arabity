using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ArabityAuth.Models
{
    public class Store
    {
        public string? Id { get; set; } = null;

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string Description { get; set; } = null!;

        public string Governorate { get; set; } = null!;


        public string City { get; set; } = null!;

        [MaxLength(11)]
        public string PhoneNumbre { get; set; } = null!;

        [MaxLength(80)]
        public string WorkTime { get; set; } = null!;

        public string GmailAddress { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        [NotMapped]
        public string UserName { get; set; }

        public virtual ICollection<StoreCarType> StoreCarTypes { get; } = new List<StoreCarType>();

        public virtual ICollection<StoreProduct> StoreProducts { get; } = new List<StoreProduct>();

        public virtual ICollection<StoreRatting> StoreRattings { get; } = new List<StoreRatting>();

        public virtual ICollection<StoreComment> StoreComments { get; } = new List<StoreComment>();

        public virtual ICollection<OrderStore> OrderStores { get; } = new List<OrderStore>();
    }
}
