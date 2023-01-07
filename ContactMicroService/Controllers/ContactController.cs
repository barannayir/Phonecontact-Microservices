using ContactMicroService.Entities;
using ContactMicroService.Repositories;
using ContactMicroService.Repositories.Interfaces;
using ContactMicroService.Services;
using ContactMicroService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactController> _logger;
        private readonly IReportRequest _reportRequest;

        public ContactController(IContactRepository contactRepository ,ILogger<ContactController> logger, ReportRequest reportRequest)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _reportRequest = reportRequest;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _contactRepository.GetContacts();
            return Ok(contacts);
        }

        [HttpGet("{uuid:length(24)}", Name = "GetContact")]
        [ProducesResponseType((int)(HttpStatusCode.NotFound))]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Contact>> Get(string uuid)
        {
            var contact = await _contactRepository.GetContact(uuid);
            if (contact == null)
            {
                _logger.LogError($"Contact with uuid: {uuid}, not found.");
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact)
        {
            await _contactRepository.CreateContact(contact);
            return CreatedAtRoute("GetContact", new { uuid = contact.uuid }, contact);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Contact>> CreateReportRequest([FromBody] Contact contact)
        {
            return Ok(await _reportRequest.SendReportRequest(contact));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateContact([FromBody] Contact contact)
        {
            return Ok(await _contactRepository.UpdateContact(contact));
        }


        [HttpDelete("{uuid:length(24)}")]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> DeleteContactById(string uuid)
        {
            return Ok(await _contactRepository.DeleteContact(uuid));
        }
    }
}
