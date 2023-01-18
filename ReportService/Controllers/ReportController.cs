using Microsoft.AspNetCore.Mvc;
using ReportService.Entities;
using ReportService.Entities.Dtos;
using ReportService.Repositories.Interfaces;
using Shared.BaseController;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : CustomBaseController
    {
        private readonly IReportRepository _reportService;
        private readonly IRabbitMQPublisherService _rabbitMQPublisherService;

        public ReportController(IReportRepository reportService, IRabbitMQPublisherService rabbitMQPublisherService)
        {
            _reportService = reportService;
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _reportService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _reportService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var response = await _reportService.CreateAsync();
            _rabbitMQPublisherService.Publish(new CreateReportEvent(response.Data.Id),
                Constant.ReportQueue, Constant.ReportRouting, Constant.ReportExchange);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ReportDto report)
        {
            var response = await _reportService.UpdateAsync(report);
            return CreateActionResultInstance(response);
        }
    }
}