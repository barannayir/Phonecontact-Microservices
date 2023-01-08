using Shared.Dtos;

namespace ContactMicroService.Entities.Dtos
{
    public class CommunicationCreateDto
    {
        public CommunicationType CommunicationType { get; set; }

        public string Address { get; set; }

        public string ContactId { get; set; }
    }
}