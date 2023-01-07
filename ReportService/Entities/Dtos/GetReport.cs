using System;

namespace ReportService.Entities.Dtos
{
    public class GetReport
    {
        public string Uuid { get; set; }
        public DateTime Date { get; set; }
        public ReportStatus ReportStat { get; set; }

        public string ReportStatusText
        {
            get
            {
                return ReportStat.ToString();
            }
        }
    }
}
