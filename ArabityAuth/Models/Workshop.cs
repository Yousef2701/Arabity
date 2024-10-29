using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ArabityAuth.Models
{
    public class Workshop
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

        public virtual ICollection<WorkshopComment> CentreComments { get; } = new List<WorkshopComment>();

        public virtual ICollection<WorkshopCarType> CentreCarTypes { get; } = new List<WorkshopCarType>();

        public virtual ICollection<WorkshopRatting> CentreRattings { get; } = new List<WorkshopRatting>();
    }
}
