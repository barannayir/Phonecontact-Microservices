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
        private readonly IMapper _mockMapper;
        private readonly ContactRepository _contactRepository;
        private readonly CommunicationRepository _communicationRepository;
        
        public CommunicationControlerTest()
        {
          
        }
        [Fact]
        public async Task GetAll_return_ok()
        {
            //Arrange
            var mockRepo = new Mock<ICommunicationRepository>();
            var _mockMapper = new Mock<IMapper>();

            var communications = new List<CommunicationDto>
 {
     new CommunicationDto { CommunicationType = CommunicationType.EMAIL, Address = "test@gmail.com" },
new CommunicationDto { CommunicationType = CommunicationType.PHONE, Address = "555-555-5555" }
    };
            _mockMapper.Setup(x => x.Map<List<CommunicationDto>>(It.IsAny<List<Communication>>())).Returns(communications);
            var response = new Response<List<CommunicationDto>> { Data = communications };
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(response);
            var _controller = new CommunicationController(mockRepo.Object);

            //Act
            var result = await _controller.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var responseData = (result as ObjectResult).Value as Response<List<CommunicationDto>>;
            Assert.Equal(communications, responseData.Data);
        }



        [Fact]
        public async Task GetAllById_return_ok()
        {
            //Arrange
            var mockRepo = new Mock<ICommunicationRepository>();
            var _mockMapper = new Mock<IMapper>();
            var communicationId = "test_communication_id";

            var communication = new CommunicationDto
            {
                Id = communicationId,
                CommunicationType = CommunicationType.EMAIL,
                Address = "test@gmail.com"
            };
            _mockMapper.Setup(x => x.Map<CommunicationDto>(It.IsAny<Communication>())).Returns(communication);
            var response = new Response<CommunicationDto> { Data = communication };
            mockRepo.Setup(repo => repo.GetByIdAsync(communicationId)).ReturnsAsync(response);
            var _controller = new CommunicationController(mockRepo.Object);

            //Act
            var result = await _controller.GetById(communicationId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var responseData = (result as ObjectResult).Value as Response<CommunicationDto>;
            Assert.Equal(communication, responseData.Data);
        }

        [Fact]
        public async Task Create_return_ok()
        {
            //Arrange
            var mockRepo = new Mock<ICommunicationRepository>();
            var _mockMapper = new Mock<IMapper>();

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
            mockRepo.Setup(repo => repo.CreateAsync(communication)).ReturnsAsync(response);
            var _controller = new CommunicationController(mockRepo.Object);

            //Act
            var result = await _controller.Create(communication) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<CommunicationDto>>(result.Value);
            var responseData = (result.Value as Response<CommunicationDto>).Data;
            Assert.Equal(responseData.Address, communication.Address);
        }

    }
}
