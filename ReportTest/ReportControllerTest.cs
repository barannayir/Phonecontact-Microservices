using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReportService.Controllers;
using ReportService.Entities;
using ReportService.Entities.Dtos;
using ReportService.Repositories;
using ReportService.Repositories.Interfaces;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ReportTest
{
    public class ReportControllerTest
    {
        private readonly Mock<IReportRepository> _mockRepo = new Mock<IReportRepository>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<IRabbitMQPublisherService> _mockRabbitMq = new Mock<IRabbitMQPublisherService>();


        [Fact]
        public async Task GetAll_success()
        {
            //Arrange

            var reportDtos = new List<ReportDto>
    {
        new ReportDto
        {
            Id = Guid.NewGuid().ToString(),
            FilePath = "",
            CreatedDate = DateTime.Now,
            Status = ReportStatusType.WAITING
        },
        new ReportDto
        {
            Id = Guid.NewGuid().ToString(),
            FilePath = "",
            CreatedDate = DateTime.Now,
            Status = ReportStatusType.WAITING
        }
    };
            _mockMapper.Setup(x => x.Map<List<ReportDto>>(It.IsAny<List<Report>>())).Returns(reportDtos);
            var response = new Response<List<ReportDto>> { Data = reportDtos };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(response);
            var _controller = new ReportController(_mockRepo.Object, _mockRabbitMq.Object);

            //Act
            var result = await _controller.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var responseData = (result as ObjectResult).Value as Response<List<ReportDto>>;
            Assert.Equal(reportDtos, responseData.Data);
        }
        [Fact]
        public async Task Create_success()
        {

            //Arrange

            var reportDtos = FakeReport();
            var response = new Response<ReportDto> { Data = reportDtos };
            _mockRepo.Setup(repo => repo.CreateAsync()).ReturnsAsync(response);
            var _controller = new ReportController(_mockRepo.Object, _mockRabbitMq.Object);

            //Act
            var result = await _controller.CreateReport() as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<ReportDto>>(result.Value);
            var responseData = (result.Value as Response<ReportDto>).Data;
            Assert.Equal(responseData.Id, reportDtos.Id);


        }
        [Fact]
        public async Task GetReportById_success()
        {
            //Arrange
            var reportDto = FakeReport();
            var response = new Response<ReportDto> { Data = reportDto };
            _mockRepo.Setup(repo => repo.GetByIdAsync(reportDto.Id)).ReturnsAsync(response);
            var _controller = new ReportController(_mockRepo.Object, _mockRabbitMq.Object);

            //Act
            var result = await _controller.GetById("testid") as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<ReportDto>>(result.Value);
            var responseData = (result.Value as Response<ReportDto>).Data;
            Assert.Equal(reportDto.Id, responseData.Id);
        }

        [Fact]
        public async Task Update_success()
        {
            //Arrange

            var reportDto = FakeReport();

            var response = new Response<NoContent> { Data = new NoContent(), StatusCode = 204 };
            _mockRepo.Setup(repo => repo.UpdateAsync(reportDto)).ReturnsAsync(response);
            var _controller = new ReportController(_mockRepo.Object, _mockRabbitMq.Object);

            //Act
            var result = await _controller.Update(reportDto) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
            Assert.IsType<Response<NoContent>>(result.Value);
        }

        public ReportDto FakeReport()
        {
            return new ReportDto
            {
                Id = "testid",
                FilePath = "",
                CreatedDate = DateTime.Now,
                Status = ReportStatusType.WAITING
            };
        }
        
    }

}
