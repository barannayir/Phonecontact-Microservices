using ContactMicroService.Entities.Dtos;
using ContactMicroService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationController : CustomBaseController
    {
        private readonly ICommunicationRepository _communicationService;

        public CommunicationController(ICommunicationRepository communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _communicationService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var response = await _communicationService.GetByIdAsync(userId);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("GetAllByContactId/{contactId}")]
        public async Task<IActionResult> GetAllByContactId(string contactId)
        {
            var response = await _communicationService.GetAllByContactIdAsync(contactId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommunicationCreateDto communication)
        {
            var response = await _communicationService.CreateAsync(communication);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CommunicationUpdateDto communication)
        {
            var response = await _communicationService.UpdateAsync(communication);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(string id)
        {
            var response = await _communicationService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}