using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Repository
{
    public abstract class BaseEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }

        [Display(Name = "تاريخ الانشاء")]
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "انشاء بواسطة")]
        public string? CreatedBy { get; set; }
        [Display(Name = "تاريخ التعديل")]
        public DateTime? UpdatedAt { get; set; }
        [Display(Name = "تعديل بواسطة")]
        public string? UpdatedBy { get; set; }
        [Display(Name = "محذوف")]
        public bool IsDeleted { get; set; }

        public bool Delete(string userName)
        {
            IsDeleted = true;
            UpdatedBy = userName;
            UpdatedAt = DateTime.Now;
            return IsDeleted;
        }

        public void create(string user)
        {
            CreatedAt = DateTime.Now;
            CreatedBy = user;
        }

        public void update(string user)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = user;
        }
    }
}
