using ReportService.Entities;
using ReportService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportServiceTests.Services
{
    internal class RabitMQTestService : IRabbitMQPublisherService
    {
        public void Publish(CreateReportEvent createReportEvent, string queue, string routing, string exchange)
        {

        }
    }
}
