using Microsoft.AspNetCore.Mvc;
using System;
using ReportService.Services.Interfaces;
using ContactMicroService.Entities;
using ContactMicroService.Data.Interfaces;
using System.Threading.Tasks;
using ContactMicroService.Services;
using ReportMicroService.Entities;
using ReportService.Entities;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly IReportService _reportService;
        private readonly IMessageProducer _messagePublisher;
        private readonly IContactContext _contactContext;


        [Route("ReportRequest")]
        [HttpGet]
        public async Task<IActionResult> GetReportRequest([FromBody] Entities.Dtos.ReportRequest request)
        {
            try
            {
                var reportId = await _reportService.Add(request);
                _messagePublisher.SendMessage(new GenerateReport
                {
                    ReportId = reportId,
                    Status = "ReportCreating"
                });
                return Ok(
                     "Report request sent " + reportId.ToString()
                );

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
