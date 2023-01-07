using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ContactMicroService.Entities
{
    public class Report
    {
        public int uuid { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsCompleted { get; set; }

    }

}
