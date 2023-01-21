using AutoMapper;
using ContactMicroService.Controllers;
using ContactMicroService.Data;
using ContactMicroService.Data.Interfaces;
using ContactMicroService.Entities;
using ContactMicroService.Entities.Dtos;
using ContactMicroService.Repositories;
using ContactMicroService.Repositories.Interfaces;
using ContactMicroService.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContactTest
{
    public class CommunicationControlerTest
    {
        private readonly Mock<ICommunicationRepository> _mockRepo = new Mock<ICommunicationRepository>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        [Fact]
        public async Task GetAll_success()
        {
            //Arrange

            var communications = new List<CommunicationDto>
 {
     new CommunicationDto { CommunicationType = CommunicationType.EMAIL, Address = "test@gmail.com" },
new CommunicationDto { CommunicationType = CommunicationType.PHONE, Address = "555-555-5555" }
    };
            _mockMapper.Setup(x => x.Map<List<CommunicationDto>>(It.IsAny<List<Communication>>())).Returns(communications);
            var response = new Response<List<CommunicationDto>> { Data = communications };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(response);
            var _controller = new CommunicationController(_mockRepo.Object);

            //Act
            var result = await _controller.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var responseData = (result as ObjectResult).Value as Response<List<CommunicationDto>>;
            Assert.Equal(communications, responseData.Data);
        }

        [Fact]
        public async Task GetAllById_success()
        {
            //Arrange
            var communicationId = "test_communication_id";

            var communication = new CommunicationDto
            {
                Id = communicationId,
                CommunicationType = CommunicationType.EMAIL,
                Address = "test@gmail.com"
            };
            _mockMapper.Setup(x => x.Map<CommunicationDto>(It.IsAny<Communication>())).Returns(communication);
            var response = new Response<CommunicationDto> { Data = communication };
            _mockRepo.Setup(repo => repo.GetByIdAsync(communicationId)).ReturnsAsync(response);
            var _controller = new CommunicationController(_mockRepo.Object);

            //Act
            var result = await _controller.GetById(communicationId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var responseData = (result as ObjectResult).Value as Response<CommunicationDto>;
            Assert.Equal(communication, responseData.Data);
        }

        [Fact]
        public async Task Update_success()
        {
            //Arrange
            var communicationDto = FakeCommunication();
            var response = new Response<NoContent> { Data = new NoContent(), StatusCode = 204 };
            _mockRepo.Setup(repo => repo.UpdateAsync(communicationDto)).ReturnsAsync(response);
            var _controller = new CommunicationController(_mockRepo.Object);
            //Act
            var result = await _controller.Update(communicationDto) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
            Assert.IsType<Response<NoContent>>(result.Value);
        }

        [Fact]
        public async Task Create_success()
        {
            //Arrange

            var communication = new CommunicationCreateDto
            {
                CommunicationType = CommunicationType.EMAIL,
                Address = "test@gmail.com"
            };
            var newCommunication = new CommunicationDto
            {
                CommunicationType = communication.CommunicationType,
                Address = communication.Address
            };
            var response = new Response<CommunicationDto> { Data = newCommunication };
            _mockMapper.Setup(x => x.Map<CommunicationDto>(It.IsAny<Communication>())).Returns(newCommunication);
            _mockRepo.Setup(repo => repo.CreateAsync(communication)).ReturnsAsync(response);
            var _controller = new CommunicationController(_mockRepo.Object);

            //Act
            var result = await _controller.Create(communication) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<CommunicationDto>>(result.Value);
            var responseData = (result.Value as Response<CommunicationDto>).Data;
            Assert.Equal(responseData.Address, communication.Address);
        }

        private CommunicationUpdateDto FakeCommunication()
        {
            return new CommunicationUpdateDto
            {
                Id = "test_communication_id",
                CommunicationType = CommunicationType.EMAIL,
                Address = "test@gmail.com"
            };
        }
    }
}
