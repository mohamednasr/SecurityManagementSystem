using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class IncomeTaxesMatrix
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "من")]
        public decimal RangeFrom { get; set; }
        [Display(Name = "إلى")]
        public decimal? RangeTo { get; set; }
        [Display(Name = "نسبه الضريبة")]
        public decimal TaxesPercentage { get; set; }

        [Display(Name = "نسبه الاعفاء")]
        public decimal TaxesExemption { get; set; } = 0;
    }
}
