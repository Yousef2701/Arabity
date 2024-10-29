using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ArabityAuth.Models
{
    public class Customer
    {
        public string? Id { get; set; } = null;

        [Display(Name ="Frist Name")]
        [AllowNull]
        [MaxLength(50)]
        public string FristName { get; set; } = null!;

        [Display(Name = "Last Name")]
        [AllowNull]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [AllowNull]
        public string ImageUrl { get; set; } = null!;


        public string Governorate { get; set; } = null!;

        public string City { get; set; } = null!;

        [Display(Name = "Gmail Address")]
        [AllowNull]
        public string GmailAddress { get; set; } = null!;

        [Display(Name = "Phone Numbre")]
        [AllowNull]
        [MaxLength(11)]
        public string PhoneNumbre { get; set; } = null!;

        [NotMapped]
        public string UserName { get; set; }

        public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

        public virtual ICollection<Favourite> Favourites { get; } = new List<Favourite>();

    }
}
