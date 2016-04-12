using Ises.Contracts.BaseCertificatesDto;
using Ises.Contracts.Common;
using Ises.Contracts.IsolationCertificatesDto;
using Ises.Contracts.WorkCertificateCategoriesDto;
using Ises.Contracts.WorkCertificatesAreasDto;
using Ises.Contracts.WorkCertificatesHazardControlsDto;
using Ises.Contracts.WorkCertificatesHazardsDto;
using Ises.Contracts.WorkCertificatesWorkCertificatesDto;
using System;
using System.Collections.Generic;

namespace Ises.Contracts.WorkCertificatesDto
{
    public class WorkCertificateDto : BaseCertificateDto
    {
        public WorkCertificateDto()
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

        public WorkCertificateCategoryDto WorkCertificateCategory { get; set; }
        public virtual ICollection<WorkCertificateHazardControlDto> HazardControls { get; set; }
        public virtual ICollection<WorkCertificateHazardDto> Hazards { get; set; }
        public virtual ICollection<IsolationCertificateDto> IsolationCertificates { get; set; }
        public virtual ICollection<WorkCertificateWorkCertificateDto> WorkCertificatesWorkCertificatesAsSource { get; set; }
        public virtual ICollection<WorkCertificateWorkCertificateDto> WorkCertificatesWorkCertificatesAsTarget { get; set; }
        public virtual ICollection<WorkCertificateAreaDto> WorkCertificateAreas { get; set; }
    }
}
