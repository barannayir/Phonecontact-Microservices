using ContactMicroService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportBackgroundService.Models
{
    public class Publish
    {
        public Report Report { get; set; }
        public List<ContactDto> Contacts { get; set; }
    }
}
