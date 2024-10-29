using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ArabityAuth.Models
{
    public class StoreProduct
    {
        [Key]
        public string Parcode { get; set; } 

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string Discreption { get; set; } = null!;

        public double Price { get; set; }

        public double Discount { get; set; }

        public int NumberOfPeices { get; set; }

        public bool Status { get; set; }

        public string DateOfPublish { get; set; } = null!;

        [AllowNull]
        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public double PriceAfterDiscount { get; set; } 


        [AllowNull]
        public string MadeIn { get; set; }

        public string? StoreId { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;

        public virtual ICollection<ProductComment> ProductComments { get; } = new List<ProductComment>();

        public virtual ICollection<ProductImage> ProductImages { get; } = new List<ProductImage>();

        public virtual ICollection<ProductRatting> ProductRattings { get; } = new List<ProductRatting>();

        public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

        public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();

        public virtual ICollection<Favourite> Favourites { get; } = new List<Favourite>();
    }
}
