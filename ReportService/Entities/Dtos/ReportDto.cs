using System;

namespace ReportService.Entities.Dtos
{
    public class ReportDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public ReportStatusType Status { get; set; }
        public string FilePath { get; set; }
    }
}