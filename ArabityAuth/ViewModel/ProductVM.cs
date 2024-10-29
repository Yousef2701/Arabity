using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ArabityAuth.ViewModel
{
    public class ProductVM
    {
        public string Parcode { get; set; }

        public int ProductQuantity { get; set; }

        public string ProductParcode { get; set; }
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

        public double PriceAfterDiscount { get; set; }


        [AllowNull]
        public string MadeIn { get; set; }


        [Display(Name = "Add a picture")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }


    public string Comment { get; set; }

    }
}
