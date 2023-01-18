using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportService.Controllers;
using ReportService.Entities.Dtos;
using ReportService.Repositories.Interfaces;
using ReportServiceTests.Services;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Controllers.Tests
{
    [TestClass()]
    public class ReportControllerTests
    {
        private readonly IReportRepository _reportService;
        private readonly IRabbitMQPublisherService _rabbitMQPublisherService;
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            _reportService = new ReportTestService();
            _rabbitMQPublisherService = new RabitMQTestService();
            _controller = new ReportController(_reportService, _rabbitMQPublisherService);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var actionResult = await _controller.GetAll();
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<List<ReportDto>>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task GetByIdTest()
        {
            var actionResult = await _controller.GetById("63c849534431c7d93359246f");
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<ReportDto>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
            Assert.IsNotNull(actualConfiguration.Data);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            ReportDto report = new ReportDto { Id = "63c849534431c7d93359246f", CreatedDate = DateTime.Now.AddHours(-1), FilePath = "TestPath1", Status = ReportService.Entities.ReportStatusType.COMPLETED };
            var actionResult = await _controller.Update(report);
            var okResult = actionResult as ObjectResult;
            var actualConfiguration = okResult.Value as Response<NoContent>;
            Assert.IsNotNull(actualConfiguration);
            Assert.IsTrue(actualConfiguration.IsSuccessful);
        }
    }
}