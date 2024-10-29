using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace ArabityAuth.ViewModel
{
    public class FinishCustomerDataVM
    {
        public string? Id { get; set; } = null;

        [Display(Name = "Frist Name")]
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

        [Display(Name = "Add a picture")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}
