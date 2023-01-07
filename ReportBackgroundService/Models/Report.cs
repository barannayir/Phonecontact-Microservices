using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBackgroundService.Models
{
    public class Report
    {
        public int uuid { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsCompleted { get; set; }
    }
}
