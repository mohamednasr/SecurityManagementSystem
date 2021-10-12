using Microsoft.AspNetCore.Http;
using MNS.Repository;
using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class EmployeeModel : BaseEntity<long>
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
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاريخ انتهاء الخدمة")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "الرقم التأميني")]
        public string InsuranceNumber { get; set; }
        [Display(Name = "المبلغ التأميني")]
        public string InsuranceAmount { get; set; }
        [Display(Name = "نسبة تحمل التأمينات")]
        public string InsurancePercentage { get; set; }
        [Display(Name = "تاريخ بداية التأمين")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? InsuranceStartDate { get; set; }
        [Display(Name = "تاريخ انتهاء التأمين")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? InsuranceEndDate { get; set; }
        [Display(Name = "الوظيفة")]
        public long JobId { get; set; }

        [Display(Name = "رقم الملف")]
        public string FileNumber { get; set; }

        [Display(Name = "تم ادراج شهاده الميلاد")]
        public bool? IsIncludeBirthCertificate { get; set; }

        [Display(Name = "ارفاق صورة ضوئية لشهاده الميلاد")]
        public IFormFile BirthCertificateCopy { get; set; }
        public string BirthCertificateCopyPath { get; set; }

        [Display(Name = "تم ادراج شهادة التجنيد")]
        public bool? IsIncludeMilitaryCertificate { get; set; }
        [Display(Name = "ارفاق صورة ضوئية لشهاده التجنيد")]
        public IFormFile MilitaryCertificateCopy { get; set; }
        public string MilitaryCertificateCopyPath { get; set; }

        [Display(Name = "تم ادراج شهادة المؤهل")]
        public bool? IsIncludeEducationCertificate { get; set; }

        [Display(Name = "ارفاق صورة ضوئية لشهاده المؤهل")]
        public IFormFile EducationCertificateSoftCopy { get; set; }
        public string EducationCertificateSoftCopyPath { get; set; }

        [Display(Name = "تم ادراج صورة البطاقة")]
        public bool IsIncludeIDCopy { get; set; }

        [Display(Name = "ارفاق صورة ضوئية لبطاقة الرقم القومي")]
        public IFormFile IDSoftCopy { get; set; }
        public string IDSoftCopyPath { get; set; }

        [Display(Name = "تم ادراج صور شخصية")]
        public bool? IsIncludePersonalPhotos { get; set; }

        [Display(Name = "ارفاق صورة شخصية")]
        public IFormFile PersonalPhotoSoftCopy { get; set; }
        public string PersonalPhotoSoftCopyPath { get; set; }

        [Display(Name = "تم ادراج كعب العمل")]
        public bool IsIncludeWorkStub { get; set; }

        [Display(Name = "ارفاق صورة ضوئية لكعب العمل")]
        public IFormFile WorkStubSoftCopy { get; set; }
        public string WorkStubSoftCopyPath { get; set; }

        [Display(Name = "تم ادراج الفيش الجنائي")]
        public bool IsIncludeCriminalCertificate { get; set; }

        [Display(Name = "ارفاق صورة ضوئية للفيش الجنائي")]
        public IFormFile CriminalCertificateSoftCopy { get; set; }
        public string CriminalCertificateSoftCopyPath { get; set; }
        [Display(Name = "الموقع")]
        public long SiteId { get; set; }

        [Display(Name = "قيمه الفتره")]
        public decimal ShiftSalary { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "الوظيفة")]
        public JobsEntity Job { get; set; }

        public SitesEntity Site { get; set; }

        public EmployeeModel convertToModel(EmployeesEntity entity)
        {
            var employee = new EmployeeModel()
            {
                Id = entity.Id,
                BirthCertificateCopyPath = entity.BirthCertificateCopy,
                CriminalCertificateSoftCopyPath = entity.CriminalCertificateSoftCopy,
                EducationCertificateSoftCopyPath = entity.EducationCertificateSoftCopy,
                IDSoftCopyPath = entity.IDSoftCopy,
                MilitaryCertificateCopyPath = entity.MilitaryCertificateCopy,
                PersonalPhotoSoftCopyPath = entity.PersonalPhotoSoftCopy,
                WorkStubSoftCopyPath = entity.WorkStubSoftCopy,
                EmployeeCode = entity.EmployeeCode,
                EndDate = entity.EndDate,
                FileNumber = entity.FileNumber,
                InsuranceNumber = entity.InsuranceNumber,
                InsuranceAmount = entity.InsuranceAmount,
                InsurancePercentage = entity.InsurancePercentage,
                InsuranceStartDate = entity.InsuranceStartDate,
                InsuranceEndDate = entity.InsuranceEndDate,
                IsIncludeBirthCertificate = entity.IsIncludeBirthCertificate,
                IsIncludeCriminalCertificate = entity.IsIncludeCriminalCertificate,
                IsIncludeEducationCertificate = entity.IsIncludeEducationCertificate,
                IsIncludeIDCopy = entity.IsIncludeIDCopy,
                IsIncludeMilitaryCertificate = entity.IsIncludeMilitaryCertificate,
                IsIncludePersonalPhotos = entity.IsIncludePersonalPhotos,
                IsIncludeWorkStub = entity.IsIncludeWorkStub,
                JobId = entity.JobId,
                MilitaryCertificateCopy = MilitaryCertificateCopy,
                Name = entity.Name,
                NationalId = entity.NationalId,
                Phone = entity.Phone,
                Phone2 = entity.Phone2,
                StartDate = entity.StartDate,
                IsActive = entity.IsActive
            };
            return employee;
        }

        public EmployeesEntity convertToEntity()
        {
            return new EmployeesEntity()
            {
                Id = this.Id,
                BirthCertificateCopy = this.BirthCertificateCopyPath,
                CriminalCertificateSoftCopy = this.CriminalCertificateSoftCopyPath,
                EducationCertificateSoftCopy = this.EducationCertificateSoftCopyPath,
                EmployeeCode = this.EmployeeCode,
                EndDate = this.EndDate,
                FileNumber = this.FileNumber,
                IDSoftCopy = this.IDSoftCopyPath,
                InsuranceNumber = this.InsuranceNumber,
                InsuranceAmount = this.InsuranceAmount,
                InsurancePercentage = this.InsurancePercentage,
                InsuranceStartDate = this.InsuranceStartDate,
                InsuranceEndDate = this.InsuranceEndDate,
                IsIncludeBirthCertificate = this.IsIncludeBirthCertificate,
                IsIncludeCriminalCertificate = this.IsIncludeCriminalCertificate,
                IsIncludeEducationCertificate = this.IsIncludeEducationCertificate,
                IsIncludeIDCopy = this.IsIncludeIDCopy,
                IsIncludeMilitaryCertificate = this.IsIncludeMilitaryCertificate,
                IsIncludePersonalPhotos = this.IsIncludePersonalPhotos,
                IsIncludeWorkStub = this.IsIncludeWorkStub,
                JobId = this.JobId,
                MilitaryCertificateCopy = MilitaryCertificateCopyPath,
                Name = this.Name,
                NationalId = this.NationalId,
                PersonalPhotoSoftCopy = this.PersonalPhotoSoftCopyPath,
                Phone = this.Phone,
                Phone2 = this.Phone2,
                StartDate = this.StartDate,
                WorkStubSoftCopy = this.WorkStubSoftCopyPath,
            };
        }


        public void addAttachmentsPath(Dictionary<string, string> files)
        {
            foreach (var file in files)
            {
                switch (file.Key)
                {
                    case "BirthCertificateCopy":
                        this.BirthCertificateCopyPath = file.Value;
                        break;
                    case "MilitaryCertificateCopy":
                        this.MilitaryCertificateCopyPath = file.Value;
                        break;
                    case "PersonalPhotoSoftCopy":
                        this.PersonalPhotoSoftCopyPath = file.Value;
                        break;
                    case "EducationCertificateSoftCopy":
                        this.EducationCertificateSoftCopyPath = file.Value;
                        break;
                    case "IDSoftCopy":
                        this.IDSoftCopyPath = file.Value;
                        break;
                    case "CriminalCertificateSoftCopy":
                        this.CriminalCertificateSoftCopyPath = file.Value;
                        break;
                    case "WorkStubSoftCopy":
                        this.WorkStubSoftCopyPath = file.Value;
                        break;
                    default:
                        throw new Exception("unkown file");
                        break;
                }
            }
        }


    }
}
