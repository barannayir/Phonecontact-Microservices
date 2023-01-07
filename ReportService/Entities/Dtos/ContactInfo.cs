using System;

namespace ReportService.Entities.Dtos
{
    public class ContactInfo
    {
        public Guid Uuid { get; set; }
        public ReportEntities reportEntities { get; set; }
        public string Information { get; set; }
        public Guid ContactUuid { get; set; }
        public string InformationType
        {
            get
            {
                return reportEntities.ToString();
            }
        }
    }
}
