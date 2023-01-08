using System;

namespace ReportService.Entities.Dtos
{
    public class ReportCreateDto
    {
        public DateTime CreatedDate { get; set; }
        public ReportStatusType Status { get; set; } = ReportStatusType.WAITING;
        public string FilePath { get; set; }
    }
}