using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReportService.Entities
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public ReportStatusType Status { get; set; }
        public string FilePath { get; set; }
    }

    public enum ReportStatusType
    {
        WAITING,
        INPROGRESS,
        COMPLETED,
        FAILED
    }
}