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
    public class ContactControllerTest
    {
        private readonly Mock<IContactRepository> _mockRepo = new Mock<IContactRepository>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        [Fact]
        public async Task Create_success()
        {
            //Arrange


            var contactDtos = new ContactCreateDto
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Company = "TestCompany"
            };
            var newContact = new ContactDto
            {
                FirstName = contactDtos.FirstName,
                LastName = contactDtos.LastName,
                Company = contactDtos.Company
            };
            var response = new Response<ContactDto> { Data = newContact };
            _mockMapper.Setup(x => x.Map<ContactDto>(It.IsAny<Contact>())).Returns(newContact);
            _mockRepo.Setup(repo => repo.CreateAsync(contactDtos)).ReturnsAsync(response);
            var _controller = new ContactController(_mockRepo.Object);

            //Act
            var result = await _controller.Create(contactDtos) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<ContactDto>>(result.Value);
            var responseData = (result.Value as Response<ContactDto>).Data;
            Assert.Equal(responseData.FirstName, contactDtos.FirstName);
        }


        [Fact]
        public async Task GetAll_success()
        {
            //Arrange

            var contactDtos = new List<ContactDto>
 {
        new ContactDto {   FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Company = "TestCompany"},
        new ContactDto {   FirstName = "TestFirstName2",
                    LastName = "TestLastName2",
                    Company = "TestCompany2" }
    };
            _mockMapper.Setup(x => x.Map<List<ContactDto>>(It.IsAny<List<Contact>>())).Returns(contactDtos);
            var response = new Response<List<ContactDto>> { Data = contactDtos };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(response);
            var _controller = new ContactController(_mockRepo.Object);

            //Act
            var result = await _controller.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            var responseData = (result as ObjectResult).Value as Response<List<ContactDto>>;
            Assert.Equal(contactDtos, responseData.Data);
        }
        [Fact]
        public async Task GetContactById_success()
        {
            //Arrange

            var contactDto = new ContactWithCommunicationsDto
            {
                Id = "63cbd98826e670467dcc2e13",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Company = "TestCompany",
                Communications = new List<CommunicationDto> { new CommunicationDto { Id = "test_communication_id" } }
            };

            _mockMapper.Setup(x => x.Map<ContactWithCommunicationsDto>(It.IsAny<Contact>())).Returns(contactDto);

            _mockRepo.Setup(repo => repo.GetById(contactDto.Id)).ReturnsAsync(Response<ContactWithCommunicationsDto>.Success(contactDto, 200));

            var _controller = new ContactController(_mockRepo.Object);

            //Act
            var result = await _controller.GetById(contactDto.Id) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Response<ContactWithCommunicationsDto>>(result.Value);
            var responseData = (result.Value as Response<ContactWithCommunicationsDto>).Data;
            Assert.Equal(contactDto.Id, responseData.Id);
            Assert.NotNull(responseData.Communications);
            Assert.Equal(1, responseData.Communications.Count);
        }

    }
}
