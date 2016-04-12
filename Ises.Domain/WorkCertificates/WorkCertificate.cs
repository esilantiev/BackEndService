using System;
using System.Collections.Generic;
using Ises.Contracts.Common;
using Ises.Domain.BaseCertificates;
using Ises.Domain.IsolationCertificates;
using Ises.Domain.WorkCertificatesAreas;
using Ises.Domain.WorkCertificatesHazardControls;
using Ises.Domain.WorkCertificatesHazards;
using Ises.Domain.WorkCertificatesWorkCertificates;
using WorkCertificateCategory = Ises.Domain.WorkCertificateCategories.WorkCertificateCategory;

namespace Ises.Domain.WorkCertificates
{
    public class WorkCertificate : BaseCertificate
    {
        public WorkCertificate()
        {
            CertificateType = BaseCertificateType.WorkCertificate;
        }
        public long WorkCertificateCategoryId { get; set; }

        public WorkCertificateStatus Status { get; set; }
        public WorkCertificateType Type { get; set; }
        public string EquipmentDescription { get; set; }
        public WorkCertificateIsolationType IsolationType { get; set; }
        public DateTime StartDate { get; set; }
        public int WorkersCount { get; set; }
        public int WorkHoursCount { get; set; }
        public string MaintenanceNumber { get; set; }
        public YesNoOption IsRal2Required { get; set; }

        public WorkCertificateCategory WorkCertificateCategory { get; set; }
        public virtual ICollection<WorkCertificateHazardControl> HazardControls { get; set; }
        public virtual ICollection<WorkCertificateHazard> Hazards { get; set; }
        public virtual ICollection<IsolationCertificate> IsolationCertificates { get; set; }
        public virtual ICollection<WorkCertificateWorkCertificate> WorkCertificatesWorkCertificatesAsSource { get; set; }
        public virtual ICollection<WorkCertificateWorkCertificate> WorkCertificatesWorkCertificatesAsTarget { get; set; }
        public virtual ICollection<WorkCertificateArea> WorkCertificateAreas { get; set; }
    }
}
