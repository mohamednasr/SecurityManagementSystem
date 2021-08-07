using MNS.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Employees")]
    public class EmployeesEntity : BaseEntity<long>
    {
        [Required]
        [Display(Name = "أسم الموظف")]
        public string Name { get; set; }
        [Display(Name = "كود الموظف")]
        public string EmployeeCode { get; set; }
        [MaxLength(14, ErrorMessage = "الرقم القومى لا يتجاوز ال14 رقم")]
        [Display(Name = "الرقم القومي")]
        public string NationalId { get; set; }
        [Required]
        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }
        [Display(Name = "رقم تليفون بديل")]
        public string Phone2 { get; set; }
        [Display(Name = "تاريخ بدء الخدمة")]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاريخ انتهاء الخدمة")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "الرقم التأميني")]
        public string InsuranceNumber { get; set; }
        [Display(Name = "الوظيفة")]
        public long JobId { get; set; }

        [Display(Name = "رقم الملف")]
        public string FileNumber {get;set; }

        [Display(Name = "تم ادراج شهاده الميلاد")]
        public bool? IsIncludeBirthCertificate { get; set; }

        [Display(Name = "ارفاق صورة ضوئية لشهاده الميلاد")]
        public string BirthCertificateCopy { get; set; }

        [Display(Name = "تم ادراج شهادة التجنيد")]
        public bool? IsIncludeMilitaryCertificate { get; set; }
        [Display(Name = "ارفاق صورة ضوئية لشهاده التجنيد")]
        public string MilitaryCertificateCopy { get; set; }

        [Display(Name = "تم ادراج شهادة المؤهل")]
        public bool? IsIncludeEducationCertificate { get; set; }
        
        [Display(Name = "ارفاق صورة ضوئية لشهاده المؤهل")]
        public string EducationCertificateSoftCopy { get; set; }

        [Display(Name = "تم ادراج صورة البطاقة")]
        public bool IsIncludeIDCopy { get; set; }
        
        [Display(Name = "ارفاق صورة ضوئية لبطاقة الرقم القومي")]
        public string IDSoftCopy { get; set; }

        [Display(Name = "تم ادراج صور شخصية")]
        public bool? IsIncludePersonalPhotos { get; set; }
        
        [Display(Name = "ارفاق صورة شخصية")]
        public string PersonalPhotoSoftCopy { get; set; }

        [Display(Name = "تم ادراج كعب العمل")]
        public bool IsIncludeWorkStub { get; set; }

        [Display(Name = "ارفاق صورة ضوئية لكعب العمل")]
        public string WorkStubSoftCopy { get; set; }

        [Display(Name = "تم ادراج الفيش الجنائي")]
        public bool IsIncludeCriminalCertificate { get; set; }
        
        [Display(Name = "ارفاق صورة ضوئية للفيش الجنائي")]
        public string CriminalCertificateSoftCopy { get; set; }

        [Display(Name = "الوظيفة")]
        public virtual JobsEntity Job { get; set; }

    }

}
