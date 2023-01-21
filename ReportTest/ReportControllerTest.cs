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
        private readonly IMapper _mockMapper;
        private readonly ReportRepository _contactRepository;


        [Fact]
        public async Task GetAll_return_ok()
        {
            //Arrange
            var mockRepo = new Mock<IReportRepository>();
            var _mockMapper = new Mock<IMapper>();
            var mockRabbitMq = new Mock<IRabbitMQPublisherService>();

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
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(response);
            var _controller = new ReportController(mockRepo.Object, mockRabbitMq.Object);

            //Act
            var result = await _controller.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var responseData = (result as ObjectResult).Value as Response<List<ReportDto>>;
            Assert.Equal(reportDtos, responseData.Data);
        }
        [Fact]
        public async Task Create_return_ok()
        {

            //Arrange
            var mockRepo = new Mock<IReportRepository>();
            var _mockMapper = new Mock<IMapper>().Object;
            var mockRabbitMq = new Mock<IRabbitMQPublisherService>();

            var reportDtos = new ReportDto
            {
                Id = Guid.NewGuid().ToString(),
                FilePath = "",
                CreatedDate = DateTime.Now,
                Status = ReportStatusType.WAITING
            };
            var response = new Response<ReportDto> { Data = reportDtos };
            mockRepo.Setup(repo => repo.CreateAsync()).ReturnsAsync(response);
            var _controller = new ReportController(mockRepo.Object, mockRabbitMq.Object);

            //Act
            var result = await _controller.CreateReport() as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<ReportDto>>(result.Value);
            var responseData = (result.Value as Response<ReportDto>).Data;
            Assert.Equal(responseData.Id, reportDtos.Id);


        }
        [Fact]
        public async Task GetReportById_return_ok()
        {
            //Arrange
            var mockRepo = new Mock<IReportRepository>();
            var _mockMapper = new Mock<IMapper>().Object;
            var mockRabbitMq = new Mock<IRabbitMQPublisherService>();

            var reportDto = new ReportDto
            {
                Id = "testid",
                FilePath = "",
                CreatedDate = DateTime.Now,
                Status = ReportStatusType.WAITING
            };
            var response = new Response<ReportDto> { Data = reportDto };
            mockRepo.Setup(repo => repo.GetByIdAsync(reportDto.Id)).ReturnsAsync(response);
            var _controller = new ReportController(mockRepo.Object, mockRabbitMq.Object);

            //Act
            var result = await _controller.GetById("testid") as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<ReportDto>>(result.Value);
            var responseData = (result.Value as Response<ReportDto>).Data;
            Assert.Equal(reportDto.Id, responseData.Id);
        }

        [Fact]
        public async Task Update_return_no_content()
        {
            //Arrange
            var mockRepo = new Mock<IReportRepository>();
            var _mockMapper = new Mock<IMapper>().Object;
            var mockRabbitMq = new Mock<IRabbitMQPublisherService>();

            var reportDto = new ReportDto
            {
                Id = "testid",
                FilePath = "",
                CreatedDate = DateTime.Now,
                Status = ReportStatusType.WAITING
            };

            var response = new Response<NoContent> { Data = new NoContent(), StatusCode = 204 };
            mockRepo.Setup(repo => repo.UpdateAsync(reportDto)).ReturnsAsync(response);
            var _controller = new ReportController(mockRepo.Object, mockRabbitMq.Object);

            //Act
            var result = await _controller.Update(reportDto) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
            Assert.IsType<Response<NoContent>>(result.Value);
        }

    }

}
