using System;

namespace ReportService.Entities.Dtos
{
    public class ReportRequest
    {
        public ReportStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}
