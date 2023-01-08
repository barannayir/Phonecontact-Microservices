using MongoDB.Bson.Serialization.Attributes;
using ReportService.Entities.Dtos;
using System;
using System.Collections.Generic;
namespace ReportMicroService.Entities
{
    public class Report
    {
        public string uuid { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsCompleted { get; set; }
        public ReportStatus Status { get; set; }
        public string Location { get; set; }

    }

}
