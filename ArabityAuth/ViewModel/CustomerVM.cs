using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using ArabityAuth.Models;

namespace ArabityAuth.ViewModel
{
    public class CustomerVM
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


        [Display(Name = "Gmail Address")]
        [AllowNull]
        public string GmailAddress { get; set; } = null!;

        [Display(Name = "Phone Numbre")]
        [AllowNull]
        [MaxLength(11)]
        public string PhoneNumbre { get; set; } = null!;

        [NotMapped]
        public string UserName { get; set; }

        public IFormFile ImageFile { get; set; }

        public List<Customer> Customers { get;set; }
    }
}
