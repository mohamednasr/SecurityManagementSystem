using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNS.Repository
{
    public abstract class BaseEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
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
