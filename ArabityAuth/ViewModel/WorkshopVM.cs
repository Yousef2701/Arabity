using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ArabityAuth.ViewModel
{
    public class WorkshopVM
    {
        public string? Id { get; set; } = null;

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string Description { get; set; } = null!;

        [MaxLength(50)]
        public string Type { get; set; } = null!;


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

        public bool Toyota { get; set; }

        [Display(Name = "Add a picture")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
    }
}
